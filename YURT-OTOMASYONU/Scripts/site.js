(function ($, TweenMax) {
    "use strict";
    $.fn.animateDelete = function () {
        var that = $(this);
        TweenMax.to(
            that,
            0.5,
            {
                scale: 0,
                transformOrigin: "50% bottom",
                force3D: "auto",
                ease: Back.easeIn,
                onComplete: function () {
                    that.remove();
                }
            }
        );
    };
    $.fn.animateInsert = function () {
        var that = $(this);
        TweenMax.from(
            that,
            0.7,
            {
                scale: 0,
                transformOrigin: "50% top",
                force3D: "auto",
                ease: Bounce.easeOut,
                clearProps: "transform"
            }
        );
    };
    $.fn.animateUpdate = function () {
        var that = $(this);
        TweenMax.from(
            that,
            0.5,
            {
                alpha: 0,
                repeat: 2,
                yoyo: true,
                ease: Linear.easeNone,
                clearProps: "opacity"
            }
        );
    };
    $.fn.animateBlink = function () {
        var that = $(this);
        TweenMax.to(
            that,
            1,
            {
                alpha: 0,
                repeat: 2,
                yoyo: true,
                repeatDelay: 0.5,
                ease: Linear.easeNone,
                clearProps: "opacity"
            }
        );
    };
    $.fn.animateBlinkShort = function () {
        var that = $(this);
        TweenMax.to(
            that,
            1,
            {
                alpha: 0,
                repeat: 0,
                yoyo: true,
                repeatDelay: 0.5,
                ease: Linear.easeNone,
                clearProps: "opacity"
            }
        );
    };


    /* 
    Using this to destroy tooltip
     * jQuery - Trigger event when an element is removed from the DOM
     http://stackoverflow.com/a/10172676/179261 */
    $.event.special.destroyed = {
        remove: function (o) {
            if (o.handler) {
                o.handler.apply(this, arguments);
            }
        }
    };


    $.fn.applyTemplateSetup = function () {
        var that = $(this);

        //that.find(".k-picker-wrap > .k-input.form-control").removeClass("form-control");

        //that.find('input[type="checkbox"][data-icheck="true"],[type="radio"][data-icheck="true"]').iCheck({
        //    checkboxClass: 'icheckbox_minimal-blue',
        //    radioClass: 'iradio_minimal-blue',
        //    increaseArea: '20%' // optional
        //});
        that.find('input[type="checkbox"][data-make-switchery-small="true"]').each(function () {
            var switchery = new Switchery(this, { size: 'small', secondaryColor: "#d9534f" });
        });
        that.find('input[type="checkbox"][data-make-switchery="true"]').each(function () {
            var switchery = new Switchery(this, { secondaryColor: "#d9534f" });
        });

        that.find('form[data-ajax-form="true"]').each(function () {
            $(this).customAjaxForm();
        });
        that.find('form[data-ajax-crud-form="true"]').each(function () {
            $(this).customAjaxCrudForm();
        });


        that.find('ul.pagination a[data-ajax="true"]').each(function () {

            $(this).ajaxSearchFormPager();
        });

        that.find('.box[data-box-remote="true"]').each(function () {
            $(this).boxRemote();
        });

        that.find('textarea[data-make-cke="true"]').each(function () {
            CKEDITOR.replace(this.id, {
                language: 'tr',
                on: {
                    change: function () {
                        this.updateElement();
                    }
                }
            });
        });

        //that.find('input[data-val-length-max], textarea[data-val-length-max]').each(function () {
        //    $(this).attr('maxlength', $(this).attr('data-val-length-max'));
        //});

        //that.find('input[maxlength].show-maxlength,textarea[maxlength].show-maxlength').maxlength({
        //    alwaysShow: true,
        //    placement: 'top-left',
        //    appendToParent: true
        //});


        //that.find('[data-toggle=confirmation]').confirmation({
        //    rootSelector: '[data-toggle=confirmation]',
        //    container: 'body',
        //    singleton: true,
        //    popout: true
        //});

        //that.find("[data-toggle='tooltip']").qtip({
        //    position: {
        //        my: 'bottom center',
        //        at: 'top center',
        //        //viewport: $(window) //jQuery 3 ErrorjQuery.Deferred exception: f.getClientRects is not a function
        //    },
        //    show: {
        //        //event: false,
        //        event: 'mouseenter hover',
        //        ready: false /*do not show on ready*/
        //    },
        //    hide: {
        //        event: 'mouseleave'
        //    },
        //    //events: {
        //    //    //this hide event will remove the qtip element from body and all assiciated events, leaving no dirt behind.
        //    //    hide: function (event, api) {
        //    //        debugger;
        //    //        var t = this;
        //    //        api.destroy(true); // Destroy it immediately
        //    //    }
        //    //},
        //    style: {
        //        classes: 'qtip-jtools qtip-shadow' // Make it red... the classic error colour!
        //    }
        //});
        //that.find("[data-toggle='tooltip']").on("destroyed", function () {
        //    $(this).qtip('destroy', true);
        //});

        /**
         * MULTILEVEL DROPDOWN
         */
        that.find(".dropdown-menu > li > a.trigger").on("click", function (e) {
            var current = $(this).next();
            var grandparent = $(this).parent().parent();
            if ($(this).hasClass('right-caret') || $(this).hasClass('left-caret'))
                $(this).toggleClass('left-caret right-caret');
            grandparent.find('.right-caret').not(this).toggleClass('left-caret right-caret');
            grandparent.find(".sub-menu:visible").not(current).hide();
            current.toggle();
            e.stopPropagation();
        });
        that.find(".dropdown-menu > li > a:not(.trigger)").on("click", function () {
            var root = $(this).closest('.dropdown');
            root.find('.right-caret').toggleClass('left-caret right-caret');
            root.find('.sub-menu:visible').hide();
        });

        /**
         * Dropdowns under table responsive does not show all. Sometimes you have to scroll
         * Fix that with next lines
         */
        that.find('.table-responsive').on('show.bs.dropdown', function () {
            that.find('.table-responsive').css("overflow", "inherit");
        });

        that.find('.table-responsive').on('hide.bs.dropdown', function () {
            that.find('.table-responsive').css("overflow", "auto");
        });







    };

})(jQuery, TweenMax);
$(function () {
    $.ajaxSetup({ cache: false });
    $(document).ajaxError(function (event, jqXhr/*request*/, settings) {

        if (jqXhr.status === 0 && (jqXhr.statusText === 'abort' || jqXhr.statusText === 'error')) {
            /*aborted or navigated to other url, before completing ajax call (mostly), ERR_CONNECTION_REFUSED so ignore and exit*/
            return;
        }
        if ((jqXhr.status === 401 || jqXhr.status === 403) && jqXhr.getResponseHeader('PRAPAZAR-NO-SUBSCRIPTION')) {
            /*Ok customer has no valid subscription, redirect to subscription page*/
            toastr.error(decodeURIComponent(jqXhr.getResponseHeader('PRAPAZAR-NO-SUBSCRIPTION')));
            return;
        }
        if ((jqXhr.status === 401 || jqXhr.status === 403 || jqXhr.status === 420) && jqXhr.responseText && jqXhr.responseText !== '') {
            toastr.error(jqXhr.responseText, jqXhr.status);
        } else if (jqXhr.status === 500) {
            toastr.error('Sunucu hatası!', '500');
        } else {
            /*kendo upload error*/
            if (jqXhr.status !== 200 && jqXhr.statusText !== 'parseerror') {
                toastr.error(jqXhr.statusText, jqXhr.status);
            }

        }
    });





    /*Master detail tables,  detail expand-collapse*/
    $('body').on('click', 'tr.master-row  .expand-details', function () {
        $(this).closest('.master-row').nextUntil('tr.master-row').slideToggle(50);
        $('span', $(this)).toggleClass('fa-chevron-right').toggleClass('fa-chevron-down');
    });

    $('body').on('shown.bs.dropdown',
        '.dropdown',
        function () {
            var menu = $(this).find('.dropdown-menu');
            var menuLeft = menu.offset().left;
            var menuWidth = menu.outerWidth();
            var documentWidth = $('body').outerWidth();
            if (menuLeft + menuWidth > documentWidth) {
                menu.offset({ 'left': documentWidth - menuWidth });
            }
        });


    $('body').applyTemplateSetup();

    // ===== Scroll to Top ==== 
    $(window).scroll(function () {
        if ($(this).scrollTop() >= 50) {        // If page is scrolled more than 50px
            $('#return-to-top').fadeIn(200);    // Fade in the arrow
        } else {
            $('#return-to-top').fadeOut(200);   // Else fade out the arrow
        }
    });

    $('#return-to-top').click(function () {      // When arrow is clicked
        $('body,html,.modal').animate({
            scrollTop: 0                       // Scroll to top of body
        }, 500);
    });

    $(document).on('click',
        function (e) {
            $('[data-toggle="popover"],[data-original-title]').each(function () {
                //the 'is' for buttons that trigger popups
                //the 'has' for icons within a button that triggers a popup
                if (!$(this).is(e.target) &&
                    $(this).has(e.target).length === 0 &&
                    $('.popover').has(e.target).length === 0) {
                    (($(this).popover('hide').data('bs.popover') || {}).inState || {}).click =
                        false; // fix for BS 3.3.6
                }

            });
        });
});