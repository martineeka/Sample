$(document).ready(function () {

    //$('div.k-grid-content').slimScroll({
    //    height: 'auto'
    //});

    $("#menu-toggle").click(function (e) {
        e.preventDefault();
        $("#wrapper").toggleClass("toggled");
        $("nav").css("margin-left", "0");
        $("i.fa-bars").toggleClass("rotated");
    });

    $("a.master-nav").click(function (e) {
        e.preventDefault();
        $("ul.master-subnav > li", $(this).parent()).toggle(400);
    });

    $("#grid .k-grid-content").css("overflow-x", "scroll").scroll(function () {
        var left = $(this).scrollLeft();
        var wrap = $("#grid > .k-grid-header-wrap");
        if (wrap.scrollLeft() != left) wrap.scrollLeft(left);
    });

    $(".full-size-list .k-grid-content").css("height", $(document).height() - 240);
    $(".hover-to-readonly").initialize(function () {
        var box = $(".hover-to-readonly");
        box.mouseenter(function () {
            $(this).prop("readonly", true);
        });
        box.mouseleave(function () {
            if (!box.is(":focus")) {
                $(this).prop("readonly", false);
            }
        });
        box.focus(function () {
            $(this).prop("readonly", true);
        });
        box.focusout(function () {
            $(this).prop("readonly", false);
        });
    })


    //disable all datepicker when initialize
    $(".k-datepicker").initialize(function () {
        var $this = $(this).children(".k-picker-wrap").children("input[data-role='datepicker']");
        $($this).prop("readonly", true);
    });

    $("div.k-grid-header").css("padding-right", "0");
    $(".phoneNumber").initialize(function () {
        $(".phoneNumber").bind('keyup blur', function () {
            var node = $(this);
            if (!(node.val() == null || node.val() == '')) {
                node.val(node.val()[0].replace(/[^0-9-+]/g, '') + node.val().substring(1, node.val().length).replace(/[^0-9]/g, ''));
            }
        });
    });
    $(".postalCode").initialize(function () {
        $(".postalCode").bind('keyup blur', function () {
            var node = $(this);
            node.val(node.val().replace(/[^0-9]/g, ''));
        });
    });
});

jQuery.fn.extend({
    disableBtn: function () {
        return this.each(function () {
            var $this = $(this).html();
            $(this).html("<i class='fa fa-circle-o-notch fa-spin'></i> Processing...");
            $(this).prop("origin-text", $this);
            $(this).attr("disabled", true);
            $(this).addClass("k-state-disabled");
        });
    },

    enableBtn: function () {
        return this.each(function () {
            if ($(this).prop("origin-text") != null)
                $(this).html($(this).prop("origin-text"));
            $(this).attr("disabled", false);
            $(this).removeClass("k-state-disabled");
        });
    }
});

function datePicker_clearValue(e) {
    var $this = $(e).siblings("span.k-widget");
    $this = $($this).children("span.k-picker-wrap").children("input");
    var datePicker;
    if ($($this).data("role") == "datepicker") {
        $this = $this.data("role", "datepicker");
        datePicker = $($this).data("kendoDatePicker");
        datePicker.value("");
    } else if ($($this).data("role") == "timepicker") {
        $this = $this.data("role", "timepicker");
        datePicker = $($this).data("kendoTimePicker");
        datePicker.value("");
    } else if ($($this).data("role") == "datetimepicker") {
        $this = $this.data("role", "datetimepicker");
        datePicker = $($this).data("kendoDateTimePicker");
        datePicker.value("");
    }
}
function clearBrowseClick(ele) {
    var his = $(ele).siblings("input");
    $.each(his, function (index) {
        $(his[index]).val('');
    })
}
function gridDataBound(e) {

    var $this = this;
    var $element;
    var $table;
    if (typeof $this.element == "undefined") {
        $element = e.sender.element;
        $table = e.sender.table;
    } else {
        $element = $this.element;
        $table = $this.table;
    }

    $($element).append("<div class='popover left' style=\"display:none\" id='popover-row-description'> <div class='arrow'></div> <div class='popover-content'> <p></p> </div> </div>");
    $($element).append("<div class='popover-dropdown' style=\"display:none\"><ul></ul></div>");

    $("body").off("click");
    $("body").on("click", function (event) {

        var gridBody = $(event.target).parent().parent();
        var buttonGrid = $(event.target).parent().parent().parent().parent();
        var buttonCaret = $(event.target).parent().parent().parent().parent().parent();

        if (gridBody[0] != $($table).find("tbody")[0]) {

            var popover = $element.find(".popover");
            if (!$(popover).is(event.target) && $(popover).has(event.target).length === 0 && $(".popover").has(event.target).length === 0) {
                $(popover).css("display", "none");
            }

        }
        if (buttonGrid[0] != $($table).find("tbody")[0] && buttonCaret[0] != $($table).find("tbody")[0]) {
            var popoverDropdown = $element.find('div.popover-dropdown');

            if (!$(popoverDropdown).is(event.target) && $(popoverDropdown).has(event.target).length === 0 && $(".popover-dropdown").has(event.target).length === 0) {
                $element.find('div.popover-dropdown').css("display", "none");
            }
        }
    });

    $($table).off("click", "button.dropdown-toggle");
    $($table).on("click", "button.dropdown-toggle", function (e) {

        var caret = $(e.target).parent()
        if (e.target == this || this == caret[0]) {

            var getParent = $(this).parent().parent().parent();
            var top = getParent.position().top;
            var left = $(this).parent().position().left;
            var offsetTop = 95;

            if (!$element.find("div.k-grid-header").length) {
                offsetTop -= $element.find("table thead").height();
            }

            var popoverDropdown = $element.find('.popover-dropdown');
            var list = $(this).siblings('ul.actions-menu').html();

            if (list != undefined && list.length != 0) {
                if (popoverDropdown.css("display") == "none") {
                    popoverDropdown.show();
                    if (window.matchMedia("(max-width: 550px)").matches) {
                        if ($(this).parent().parent().prev().length != 0) {
                            $(popoverDropdown).css("top", top + 38 + 33 + "px");

                        } else {
                            $(popoverDropdown).css("top", top + 38 + "px");
                        }


                    } else {
                        $(popoverDropdown).css("top", top + offsetTop + "px");
                    }
                    $(popoverDropdown).css("left", left + "px");
                    popoverDropdown.find('ul').html(list);
                } else {
                    popoverDropdown.hide();
                }
            }

        }

    });

    $($table).off("click", "tr td");
    $($table).on("click", "tr td", function (ek) {
        if (ek.target == this) {
            if (window.matchMedia("(min-width: 550px)").matches) {
                var thisPositon = $(this).position();

                var marginTop = 60;
                if (!$element.find("div.k-grid-header").length) {
                    marginTop -= $element.find("table thead").height();
                }

                var popover = $element.find(".popover");
                $(popover).css("display", "none");

                var description = $(this).siblings("td.row-description");
                if (description.length !== 0) {
                    $(popover).css("display", "block");
                    $(popover).css("top", marginTop + thisPositon.top + "px");
                    $(popover).find("p").html(description.html());
                }
            }
        }
    });

    if ($($element).parent().find("#list-all-item").length || ($($element).parent().find("#list-all-item").length && $($element).parent().find("#list-all-item > option").length == 0)) {
        refreshPool(e); // uncomment this for implementtion
        $($element).parents(".general-table-form").find("#list-check").empty();
    }

    // ketika klik cebok chkAll
    $($element).off("click", "input#chkAll");
    $($element).on("click", "input#chkAll", function () {
        var state = $(this).is(":checked");
        var checkbox = $table.find(".chkDelete").not("#chkAll");

        // check/uncheck semua cebok di grid
        checkbox.prop("checked", state);

        // masukin ke pool selected row
        if (state) {
            checkbox.each(function () {
                checked($(this).val());
            });
        } else { // otherwise remove dari pool
            checkbox.each(function () {
                $("#list-check option[value='" + $(this).val() + "']").remove();
            });
        }

        // update text selected row
        updateText();
    });

    // ini untuk retain cebok di grid ttp ter-check ketika pindah halaman
    $($element).parent().find("#list-check option").unbind();
    $($element).parent().find("#list-check option").each(function () {
        var checkbox = $table.find(".chkDelete");
        var id = $(this).val();

        // dicek berdasarkan pool selected row
        checkbox.each(function () {
            if ($(this).val() == id) {
                $(this).prop("checked", true);
                return false;
            }
            return true;
        });
    });

    // defaultnya chkAll tidak tercentang
    if ($element.find("#chkAll") != null) {
        $element.find("#chkAll").prop("checked", false);
    }

    var chkDelete = $table.find(".check-box-column > .checkbox > .chkDelete");
    var chkCount = Object.keys(chkDelete).length;
    var checkedCheckbox = $table.find(".chkDelete:checked");
    var checkedCount = Object.keys(checkedCheckbox).length;

    // secara default, chkAll enabled
    $element.find("#chkAll").attr("disabled", false);

    // jika grid kosong (4 == kosong, entah kenapa -_-), maka chkAll disabled
    if (chkCount <= 4) {
        $element.find("#chkAll").attr("disabled", true);
    } else if (chkCount == checkedCount) { // kalo cebok dicek semua, chkAll kecentang
        $element.find("#chkAll").prop("checked", true);
    }

    // ketika klik cebok di grid
    $($table).off("click", ".check-box-column > .checkbox > .chkDelete");
    $($table).on("click", ".check-box-column > .checkbox > .chkDelete", function () {
        checkedCheckbox = $table.find(".chkDelete:checked");
        checkedCount = Object.keys(checkedCheckbox).length;
        chkDelete = $table.find(".check-box-column > .checkbox > .chkDelete");
        chkCount = Object.keys(chkDelete).length;
        var chkAll = $element.find("#chkAll");
        // jika chkAll tercentang dan cebok tidak tercentang, maka chkAll kecentang
        if (chkAll.is(":checked") && !$(this).is(":checked")) {
            chkAll.prop("checked", false);
        } else if (!chkAll.is(":checked") && checkedCount === chkCount) { // jika chkAll tidak tercentang dan grid kecentang semua, maka chkAll kecentang
            chkAll.prop("checked", true);
        }

        // masukin ke pool selected row
        var state = $(this).is(":checked");
        if (state) {
            checked($(this).val());
        } else { // otherwise remove dari pool
            $($element).parents(".general-table-form").find("#list-check option[value='" + $(this).val() + "']").remove();
        }

        // update text selected row
        updateText();
    });

    $($element).parent().find("#checked-refresh").unbind();
    // ketika klik tombol refresh grid
    $($element).parent().find("#checked-refresh").click(function (ev) {
        ev.preventDefault();

        var gridId = $element.attr("id");
        Chaka.setEvent(ev);
        Chaka.refreshGrid(gridId);

        // uncentang chkAll dan semua cebok di grid
        $table.find(".chkDelete").prop("checked", false);
        $element.find("#chkAll").prop("checked", false);
        $($element).find("div.popover").remove();
        $($element).find("div.popover-dropdown").remove();

    });

    $($element).parent().find("#checked-select").unbind();
    // ketika klik tombol check all on grid
    $($element).parent().find("#checked-select").click(function (ev) {
        ev.preventDefault();

        var allItem = $($element).parents(".general-table-form").find("#list-all-item > option");
        $($element).parents(".general-table-form").find("#list-check").empty();
        // masukin semua item di grid ke pool selected row -> note: ini kurang bagus performanya, mungkin ada yg bisa benerin
        //$.map(allItem, function (item) {
        //    checked(item.value);
        //});

        // note: code setTimeout dibawah ini pengganti fungsi map diatas, gak bikin browser hang, but eksekusi lbh lambat dr map
        $($element).parent().find("#checked-info-text").html("<kbd><i class='fa fa-refresh fa-spin'></i> <abbr title='Working time depends on the number of rows'><b>Working</b></abbr>... <span id='percentage'></span></kbd>");
        disabled($($element).parent().find("#checked-refresh"));
        disabled($($element).parent().find("#checked-select"));
        disabled($($element).parent().find("#checked-clear"));
        var x = 0, total = allItem.length, percentage = $($element).parent().find("#checked-info-text").find("#percentage");
        setTimeout(function checking() {
            checked(allItem[x].value);
            $(percentage).html(Math.round(x / total * 100) + "%");
            x++;
            if (x < total) {
                setTimeout(checking, 0);
            } else {
                $($element).parent().find("#checked-info-text").fadeOut("fast");
                setTimeout(function () {
                    $($element).parent().find("#checked-info-text").fadeIn("slow");
                    $($element).parent().find("#checked-info-text").html("<kbd><i class='fa fa-check'></i> All rows selected!</kbd>");
                    $($element).parent().find("#checked-info-text").fadeOut(500);
                    setTimeout(function () {
                        $($element).parent().find("#checked-info-text").fadeIn("slow");
                        updateText();
                        enabled($($element).parent().find("#checked-refresh"));
                        enabled($($element).parent().find("#checked-select"));
                        enabled($($element).parent().find("#checked-clear"));
                    }, 1000);
                }, 200);
            }
        }, 0);

        // centang chkAll dan semua cebok di grid
        $table.find(".chkDelete").not("#chkAll").prop("checked", true);
        var chkAll = $element.find("#chkAll");
        if (!chkAll.attr("disabled")) {
            $element.find("#chkAll").prop("checked", true);
        }
        //kendo.ui.progress($($element), false);
    });

    $($element).parent().find("#checked-clear").unbind();
    // ketika klik tombol remove all selection, means apus semua di pool selected row
    $($element).parent().find("#checked-clear").click(function (ev) {
        ev.preventDefault();

        // apus semua isi pool selected row
        $($element).parents(".general-table-form").find("#list-check").empty();
        disabled($($element).parent().find("#checked-refresh"));
        disabled($($element).parent().find("#checked-select"));
        disabled($($element).parent().find("#checked-clear"));
        $($element).parent().find("#checked-info-text").fadeOut("fast");
        setTimeout(function () {
            $($element).parent().find("#checked-info-text").fadeIn("slow");
            $($element).parent().find("#checked-info-text").html("<kbd><i class='fa fa-check'></i> Selection cleared!</kbd>");
            $($element).parent().find("#checked-info-text").fadeOut(500);
            setTimeout(function () {
                $($element).parent().find("#checked-info-text").fadeIn("slow");
                updateText();
                enabled($($element).parent().find("#checked-refresh"));
                enabled($($element).parent().find("#checked-select"));
                enabled($($element).parent().find("#checked-clear"));
            }, 1000);
        }, 200);

        // uncentang chkAll dan semua cebok di grid
        $table.find(".chkDelete").prop("checked", false);
        $element.find("#chkAll").prop("checked", false);
    });

    // ini fungsi untuk munculin dan update informasi selected row di atas grid
    function updateText() {
        var length = $($element).parents(".general-table-form").find("#list-check > option").length;
        if (length === 1) {
            $($element).parents(".general-table-form").find("#checked-info-text").html("<kbd><i class='fa fa-lightbulb-o'></i> " + length + " row selected</kbd>");
        } else if (length > 1) {
            $($element).parents(".general-table-form").find("#checked-info-text").html("<kbd><i class='fa fa-lightbulb-o'></i> " + length + " rows selected</kbd>");
        } else {
            $($element).parents(".general-table-form").find("#checked-info-text").html("<kbd><i class='fa fa-lightbulb-o'></i> " + length + " row(s) selected</kbd>");
        }
    }

    // ini fungsi untuk masukin row kedalam pool selected row
    function checked(id) {
        $($element).parents(".general-table-form").find("#list-check").append($("<option/>",
            {
                value: id,
                text: id
            }));
    }

    // ini fungsi buat refresh selected pool
    function refreshPool() {
        e.preventDefault();
        var parent = $($element).parent();
        //parent.find("#checked-info-text").html("<kbd><i class='fa fa-refresh fa-spin'></i> A little more...</kbd>");
        var gridId = $($element).attr("id");
        var grid = $("#" + gridId).data("kendoGrid");
        var url = grid.dataSource.transport.options.read.url.split('?')[0];
        var pool = $($element).parents(".general-table-form").find("#list-all-item");
        if (url != "") {
            $.ajax({
                type: "POST",
                async: true,
                url: url,
                success: function (result) {
                    if (result != "" && result != null) {
                        if (result.IsSuccess == undefined) {
                            window.tos = 1;
                            $.each(result.Data,
                                function (index, value) {
                                    //Untuk grid yg checkboxnya optional -> Tambahan property Check di list data
                                    if (value.Check == undefined || value.Check == true) {
                                        pool.append($("<option/>",
                                            {
                                                value: value.ID,
                                                text: value.ID
                                            }));
                                    }
                                });
                            parent.find("#checked-info-text").fadeOut("fast");
                            setTimeout(function () {
                                if ($element.find("#chkAll").length && result.Data.length > 0) {
                                    parent.find("#checked-info-text").html("<kbd><i class='fa fa-lightbulb-o'></i> 0 row(s) selected</kbd>");
                                    parent.find("#checked-info-text").fadeIn("slow");
                                    parent.find("#checked-select").show();
                                    parent.find("#checked-clear").show();
                                } else if (!$element.find("#chkAll").length && result.Data.length > 0) {
                                    parent.find("#checked-info-text").fadeOut("slow");
                                    parent.find("#checked-select").hide();
                                    parent.find("#checked-clear").hide();
                                } else if (result.Data.length == 0) {
                                    parent.find("#checked-info-text").html("<kbd><i class='fa fa-info-circle'></i> No data available</kbd>");
                                    parent.find("#checked-info-text").fadeIn("slow");
                                    parent.find("#checked-select").hide();
                                    parent.find("#checked-clear").hide();
                                }
                            }, 200);

                        } else {
                            parent.find("#checked-info-text").html("<kbd><i class='fa fa-exclamation-triangle'></i> An <abbr title='Please wait a few moment and refresh the grid. If the problem persists, contact a system administrator'><u><b>error</b></u></abbr> occured while loading data</kbd>");
                            parent.find("#checked-info-text").fadeIn("slow");
                            parent.find("#checked-select").hide();
                            parent.find("#checked-clear").hide();
                        }

                        enabled(parent.find("#checked-refresh"));
                        enabled(parent.find("#checked-select"));
                        enabled(parent.find("#checked-clear"));

                        $('.k-grid-content').doubleScroll();
                    }
                },
                error: function (err) {
                    parent.find("#checked-info-text").html("<kbd><i class='fa fa-exclamation-triangle'></i> An <abbr title='Please wait a few moment and refresh the grid. If the problem persists, contact a system administrator'><u><b>error</b></u></abbr> occured while loading data</kbd>");
                    parent.find("#checked-info-text").fadeIn("slow");
                    parent.find("#checked-select").hide();
                    parent.find("#checked-clear").hide();
                    enabled(parent.find("#checked-refresh"));
                }
            });
        }
    }

    function disabled(param) {
        $(param).attr("disabled", true);
        $(param).css("pointer-events", "none");
    }

    function enabled(param) {
        $(param).attr("disabled", false);
        $(param).css("pointer-events", "auto");
    }

    $('.k-grid-content').doubleScroll();

}

(function ($) {

    jQuery.fn.doubleScroll = function (userOptions) {

        // Default options
        var options = {
            contentElement: undefined, // Widest element, if not specified first child element will be used
            scrollCss: {
                //'overflow-x': 'auto',
                //'overflow-y': 'hidden'
            },
            contentCss: {
                //'overflow-x': 'auto',
                //'overflow-y': 'hidden'
            },
            onlyIfScroll: true, // top scrollbar is not shown if the bottom one is not present
            resetOnWindowResize: false, // recompute the top ScrollBar requirements when the window is resized
            timeToWaitForResize: 30 // wait for the last update event (usefull when browser fire resize event constantly during ressing)
        };

        $.extend(true, options, userOptions);

        // do not modify
        // internal stuff
        $.extend(options, {
            topScrollBarMarkup: '<div id="scrollabletop" class="doubleScroll-scroll-wrapper" style="height: 17px;"><div class="doubleScroll-scroll" style="height: 17px;"></div></div>',
            topScrollBarWrapperSelector: '.doubleScroll-scroll-wrapper',
            topScrollBarInnerSelector: '.doubleScroll-scroll'
        });

        var _showScrollBar = function ($self, options) {

            if (options.onlyIfScroll && $self.get(0).scrollWidth <= $self.width()) {
                // content doesn't scroll
                // remove any existing occurrence...
                $self.prev(options.topScrollBarWrapperSelector).remove();
                return;
            }

            // add div that will act as an upper scroll only if not already added to the DOM
            var $topScrollBar = $self.prev(options.topScrollBarWrapperSelector);

            if ($topScrollBar.length == 0) {

                // creating the scrollbar
                // added before in the DOM
                $topScrollBar = $(options.topScrollBarMarkup);
                $self.before($topScrollBar);

                // apply the css
                $topScrollBar.css(options.scrollCss);
                $self.css(options.contentCss);

                // bind upper scroll to bottom scroll
                $topScrollBar.bind('scroll.doubleScroll', function () {
                    $self.scrollLeft($topScrollBar.scrollLeft());
                });

                // bind bottom scroll to upper scroll
                var selfScrollHandler = function () {
                    $topScrollBar.scrollLeft($self.scrollLeft());
                };
                $self.bind('scroll.doubleScroll', selfScrollHandler);
            }

            // find the content element (should be the widest one)	
            var $contentElement;

            if (options.contentElement !== undefined && $self.find(options.contentElement).length !== 0) {
                $contentElement = $self.find(options.contentElement);
            } else {
                $contentElement = $self.find('>:first-child');
            }

            // set the width of the wrappers
            $(options.topScrollBarInnerSelector, $topScrollBar).width($contentElement.outerWidth());
            $topScrollBar.width($self.width());
            $topScrollBar.scrollLeft($self.scrollLeft());

        }

        return this.each(function () {

            var $self = $(this);

            _showScrollBar($self, options);

            // bind the resize handler 
            // do it once
            if (options.resetOnWindowResize) {

                var id;
                var handler = function (e) {
                    _showScrollBar($self, options);
                };

                $(window).bind('resize.doubleScroll', function () {
                    // adding/removing/replacing the scrollbar might resize the window
                    // so the resizing flag will avoid the infinite loop here...
                    clearTimeout(id);
                    id = setTimeout(handler, options.timeToWaitForResize);
                });

            }

        });

    }

}(jQuery));

function checkAll() {
    if ($('#chkAll').is(":checked")) {
        var checkboxes = document.getElementsByName("chkDelete");
        for (var i = 0, n = checkboxes.length; i < n; i++) {
            checkboxes[i].checked = true;
        }
    } else {
        var checkboxes = document.getElementsByName("chkDelete");
        for (var i = 0, n = checkboxes.length; i < n; i++) {
            checkboxes[i].checked = false;
        }
    }

}