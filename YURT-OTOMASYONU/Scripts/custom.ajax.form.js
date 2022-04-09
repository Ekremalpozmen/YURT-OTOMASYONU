(function ($) {
    "use strict";
    $.fn.customAjaxForm = function () {
        var that = this;

        var $targetCon = null;
        if (typeof this.attr('data-target-container') !== 'undefined') {

            $targetCon = $(this.attr('data-target-container'));
        }

        var triggerOnSuccess = that.attr('data-trigger-onsuccess') === 'true';

        var form = $(that);

        form.on('submit', function () {
            if (!form.valid()) {
                return false;
            }
            var overlay = $("<div class='page-wrapper'><div class='page-content'><div id='loader-wrapper'  class='text-center'><div id='loader' ></div></div></div></div>");
            $('body').append(overlay);
            $.ajax({
                url: form.attr("action"),
                dataType: 'json',
                type: "POST",
                cache: false,
                data: form.serialize()
            }).done(function (result) {
                $('body').find(overlay).remove();
                if (result.success) {
                    if ($targetCon && $targetCon.length > 0) {
                        /*It seems JQuery.html() slows things. Use innerHTML when possible*/
                        //$targetCon.html(result.responseText);
                        $targetCon.empty();
                        $targetCon[0].innerHTML = result.responseText;
                        $targetCon.applyTemplateSetup();

                        var $modal = $targetCon.closest('.modal');
                        if ($modal.length > 0) {
                            /*After bootstrap 3.3 we need to call this to update modals backdrop height*/
                            $modal.data('bs.modal').handleUpdate();
                        }

                    }
                    if (triggerOnSuccess) {
                        form.trigger('onSuccess', result.item);
                    }
                } else {
                    $.each(result.errorMessages, function (i, item) {
                        toastr.error(item);
                    });
                }
            }).fail(function () {
                $('body').find(overlay).remove();
            });

            return false;
        });
    };
})(jQuery);