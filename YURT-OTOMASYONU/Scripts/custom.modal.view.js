(function ($) {
    $.fn.customModalView = function () {
        var that = this;
        var overlay = $("<div class='page-wrapper'><div class='page-content'><div id='loader-wrapper'  class='text-center'><div id='loader' ></div></div></div></div>");
        $('body').append(overlay);


        var modalWindow = $("<div class='modal fade in' role='dialog' tabindex='-1'>\
                                <div class='modal-dialog'>\
                                    <div class='modal-content'>\
                                        <div class='modal-header'>\
                                          \
                                            <h4 class='modal-title'>&nbsp;</h4>\
              <button type='button' class='close' data-bs-dismiss='modal' aria-hidden='true'>&times;</button>\
                                        </div>\
                                        <div class='modal-body'>\
                                            <div class='modalContent'></div>\
                                        </div>\
                                    </div>\
                                </div>\
                            </div>");
        $('body').append(modalWindow);
        if (typeof this.attr('data-modal-title') !== 'undefined') {
            modalWindow.find('h4.modal-title').text($(this).data('modalTitle'));
        }
        if (typeof this.attr('data-no-header') !== 'undefined') {
            if (this.attr('data-no-header') === 'true') {
                modalWindow.find('.modal-header').remove();
            }
        }
        var modalContent = modalWindow.find('.modalContent');
        if (typeof this.attr('data-modal-size') !== 'undefined') {
            modalWindow.find('.modal-dialog').addClass($(this).data('modalSize'));
        }

        var hideCallbackAttribute = null;
        if (typeof this.attr('data-hide-callback') !== 'undefined') {
            hideCallbackAttribute = $(this).data('hideCallback');
        }

        modalContent.load(this.attr('href'), function (response, status, xhr) {
            if (status === 'error') {
                /*toastr.error(decodeURIComponent(xhr.statusText.replace('+', ' ')), xhr.status);*/
                $('body').find(overlay).remove();
                toastr.error(xhr.responseText, xhr.status);
                return;
            }
            modalWindow.modal('show');

            if (hideCallbackAttribute != null) {
                /* We have a hide callBack, call it*/
                modalWindow.on('hide.bs.modal', function () {

                    eval(hideCallbackAttribute);

                });
            }
            modalWindow.on('hidden.bs.modal', function () {
                //kendo.destroy($(this));
                $(this).data('bs.modal', null);
                /*This is important!!!!!*/
                $(this).remove();
                if ($('.modal:visible').length > 0) {
                    // restore the modal-open class to the body element, so that scrolling works
                    setTimeout(function () {
                        $(document.body).addClass('modal-open');
                    }, 0);
                }
            });
            $.validator.unobtrusive.parse(modalContent);
            modalContent.applyTemplateSetup();
            $('body').find(overlay).remove();
        });
    };
})(jQuery);
$(function () {
    $('body').on('click', 'a[data-modal-view="true"]', function (e) {
        $(this).customModalView();
        return false;
    });
});