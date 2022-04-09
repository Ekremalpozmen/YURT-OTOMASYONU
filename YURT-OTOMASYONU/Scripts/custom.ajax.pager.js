(function ($) {
    "use strict";
    $.fn.ajaxSearchFormPager = function () {
      
        var targetCon = false;
        if (typeof this.attr('data-ajax-update') !== 'undefined') {
            targetCon = true;
        }
        var $targetCon = targetCon ? $(this.attr('data-ajax-update')) : null;
        var $link = $(this);
        var $url = $(this).attr("href");
        $link.on('click', function () {
            var overlay = $("<div class='loader-wrapper'><div id='loadingOverlay'></div></div>");

            $('body').append(overlay);
            $('[data-toggle="popover"],[data-original-title]', $targetCon).each(function () {
                $(this).popover('hide');
            });
            $.ajax({
                url: $url,
                dataType: 'json',
                type: "POST",
                cache: false
            }).done(function (result) {
                $('body').find(overlay).remove();
                if (result.success) {
                    if ($targetCon.length > 0) {
                        /*It seems JQuery.html() slows things. Use innerHTML when possible*/
                        //$targetCon.html(result.responseText);
                        $targetCon[0].innerHTML = result.responseText;
                        $targetCon.applyTemplateSetup();
                        
                    }
                } else {
                    toastr.error(result.responseText);
                }
            }).fail(function () {
                $('body').find(overlay).remove();
            });
            return false;
        });
    };
})(jQuery);