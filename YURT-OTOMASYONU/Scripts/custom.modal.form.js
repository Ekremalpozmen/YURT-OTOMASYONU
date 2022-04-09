(function ($) {
    $.fn.customModalForm = function () {
        var that = this;
        var overlay = $("<div class='page-wrapper'><div class='page-content'><div id='loader-wrapper'  class='text-center'><div id='loader' ></div></div></div></div>");
        $('body').append(overlay);
        var modalWindow = $("<div class='modal fade in modal-form' role='dialog'>\
                                <div class='modal-dialog'>\
                                    <div class='modal-content'>\
                                        <div class='modal-header'>\
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
            modalWindow.find('h4.modal-title').html($(this).data('modalTitle'));
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
        var hookbeforeunload = false, animateItem = true, deleteItem = false, replaceItem = false, highlightItem = false, prependItem = false, appendItem = false, savemessage = false;
        if (typeof this.attr('data-modal-delete') !== 'undefined') {
            deleteItem = true;
        }
        var $deleteItem = deleteItem ? $(this.attr('data-modal-delete')) : null;

        if (typeof this.attr('data-modal-no-animation') !== 'undefined') {
            animateItem = false;
        }

        if (typeof this.attr('data-modal-update') !== 'undefined') {
            replaceItem = true;
        }

        if (typeof this.attr('data-modal-highlight') !== 'undefined') {
            highlightItem = true;
        }

        if (typeof this.attr('data-hook-beforeunload') !== 'undefined') {
            hookbeforeunload = this.attr('data-hook-beforeunload') === 'true';
        }

        var $replaceItem = null;
        if (replaceItem) {
            var attributeValue = this.attr('data-modal-update');
            if (attributeValue.indexOf('$') === 0) {
                $replaceItem = eval(attributeValue);
            } else {
                $replaceItem = $(this.attr('data-modal-update'));
            }
        }

        var $highlightItem = null;
        if (highlightItem) {
            var highlightValue = this.attr('data-modal-highlight');
            if (highlightValue.indexOf('$') === 0) {
                $highlightItem = eval(highlightValue);
            } else {
                $highlightItem = $(this.attr('data-modal-highlight'));
            }
        }

        if (typeof this.attr('data-modal-prepend') !== 'undefined') {
            prependItem = true;
        }
        var $prependItem = prependItem ? $(this.attr('data-modal-prepend')) : null;

        if (typeof this.attr('data-modal-append') !== 'undefined') {
            appendItem = true;
        }
        var $appendItem = appendItem ? $(this.attr('data-modal-append')) : null;

        if (typeof this.attr('data-modal-save-message') !== 'undefined') {
            savemessage = true;
        }

        var triggerOnSuccess = this.attr('data-modal-trigger-onsuccess') === 'true';

        var $savemessage = savemessage ? this.attr('data-modal-save-message') : '';

        $.ajax({
            url: this.attr('href'),
            dataType: 'html',/*included script tags are evaluated when inserted in the DOM.*/
            type: "GET"
        }).done(function (result) {
            /*Needed for anti cache busting resources by jquery */
            $.ajaxSetup({ cache: true });
            modalContent.html(result);
            $.ajaxSetup({ cache: false });


            modalWindow.on('show.bs.modal', function (e) {
                if (hookbeforeunload) {
                    var confirmationMessage = 'Sayfadan ayrılırsanız, kaydedilmemiş verileriniz kaybolur!';
                    (e || window.event).returnValue = confirmationMessage; //Gecko + IE
                    return confirmationMessage; //Webkit, Safari, Chrome etc.
                }

                if ($highlightItem) {
                    $highlightItem.addClass('bg-gray disabled');
                }
            });
            modalWindow.on('hide.bs.modal', function (e) {
                if (hookbeforeunload) {
                    var confirmationMessage = 'Sayfadan ayrılırsanız, kaydedilmemiş verileriniz kaybolur!';
                    (e || window.event).returnValue = confirmationMessage; //Gecko + IE
                    return confirmationMessage; //Webkit, Safari, Chrome etc.

                }

                if ($highlightItem) {
                    $highlightItem.removeClass('bg-gray disabled');
                }
            });

            modalWindow.on('shown.bs.modal', function () {
                $(this).find('[autofocus]').focus();
                /**Scroll Top*/
                $(this).scroll(function () {
                    if ($(this).scrollTop() >= 50) {        // If page is scrolled more than 50px
                        $('#return-to-top').fadeIn(200);    // Fade in the arrow
                    } else {
                        $('#return-to-top').fadeOut(200);   // Else fade out the arrow
                    }
                });


            });
            modalWindow.modal('show');

            modalWindow.on('hidden.bs.modal', function () {
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
        }).fail(function () {
            $('body').find(overlay).remove();
        });

        modalWindow.off('submit', 'form');
        modalWindow.on('submit', 'form', function (event) {
            var $submitBtn = $(this).find(":submit");
            $submitBtn.prop("disabled", true);
            var submitBtnHtml = $submitBtn.html();
            var loadingIcon = $("<span class='fa fa-sync fa-spin'></span>");
            $submitBtn.html(loadingIcon);
            var form = $(this);
            var formData = new FormData(form[0]);

            $.ajax({
                url: $(this).attr("action"),
                dataType: 'json',
                type: "POST",
                cache: false,
                data: formData,
                processData: false,
                contentType: false

            }).done(function (result) {
                if (result.success) {
                    modalWindow.modal('hide');
                    if (triggerOnSuccess) {
                        $(that).trigger('onSuccess', result.item);
                    }
                    $.each(result.warningMessages, function (i, item) {
                        toastr.error(item, 'asd');
                    });
                    $.each(result.successMessages, function (i, item) {
                        toastr.success(item, 'Başarılı');
                    });
                    var $el;
                    if (prependItem) {
                        $el = $(result.responseText);
                        $prependItem.prepend($el);
                        $el.applyTemplateSetup();
                        if (animateItem) {
                            if ($el.length > 1) {
                                $el.first().animateInsert();
                            } else {
                                $el.animateInsert();
                            }
                        }

                    } else if (appendItem) {
                        $el = $(result.responseText);
                        $appendItem.append($el);
                        $el.applyTemplateSetup();
                        if (animateItem) {
                            if ($el.length > 1) {
                                $el.first().animateInsert();
                            } else {
                                $el.animateInsert();
                            }
                        }

                    } else if (replaceItem) {
                        $el = $(result.responseText);
                        if ($replaceItem.length > 1) {
                            $replaceItem.slice(1).remove();
                            $replaceItem.slice(0, 1).replaceWith($el);
                        } else {
                            $replaceItem.replaceWith($el);
                        }
                        $el.applyTemplateSetup();
                        if (animateItem) {
                            if ($el.length > 1) {
                                $el.first().animateUpdate();
                            } else {
                                $el.animateUpdate();
                            }
                        }
                    } else if (deleteItem) {
                        if (animateItem) {
                            if ($deleteItem.length > 1) {
                                $deleteItem.first().animateDelete();
                            } else {
                                $deleteItem.animateDelete();
                            }
                        }
                    }
                    if (savemessage) {
                        toastr.success($savemessage);
                    }
                } else {
                    modalContent.empty();
                    modalContent.html(result.responseText);
                    $.each(result.errorMessages, function (i, item) {
                        toastr.error(item, 'Hata');
                    });
                    $.validator.unobtrusive.parse(modalContent);
                    modalContent.applyTemplateSetup();
                }
            }).fail(function () {
                $submitBtn.prop("disabled", false);
                $submitBtn.html(submitBtnHtml);
            });
            return false;
        });
    };
})(jQuery);
$(function () {
    $('body').on('click', 'a[data-modal-form="true"]', function (e) {
        $(this).customModalForm();
        e.preventDefault();
        //return false;
    });
});