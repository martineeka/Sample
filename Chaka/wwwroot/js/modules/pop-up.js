var popup = (function (popup) {
    var event;

    var windowOptions = {
        modal: true,
        maxWidth: popup.width,
        width: $(window).width() * 9 / 10,
        maxHeight: $(window).height() * 9 / 10,
        visible: false,
        refresh: function () { this.center(); },
        close: function (e) { $(this.element).empty(); },
        animation: {
            open: { effects: "slideIn:down fadeIn", duration: 350 },
            close: { effects: "slideIn:up fadeIn", reverse: true, duration: 200 }
        }
    };

    var windowOptionsDetail = {
        modal: true,
        maxWidth: popup.width,
        width: $(window).width() * 9 / 10,
        maxHeight: $(window).height() * 9 / 10,
        visible: false,
        refresh: function () { this.center(); },
        close: function (e) { $(this.element).empty(); },
        animation: {
            open: { effects: "slideIn:down fadeIn", duration: 350 },
            close: { effects: "slideIn:up fadeIn", reverse: true, duration: 200 }
        }
    };

    popup.getWindowOptions = function () {
        return windowOptions;
    };

    popup.getWindowOptionsDetail = function () {
        return windowOptionsDetail;
    };

    popup.addNewClick = function (e) {
        e.preventDefault();
        $(".k-window-title").html(popup.createTitle);
        $("#addEditWindow")
            .data("kendoWindow")
            .refresh({
                url: popup.createUrl
            })
            .center()
            .open();

        event = e;
    };

    popup.editClick = function (e) {
        e.preventDefault();
        $(".k-window-title").html(popup.editTitle);
        $("#addEditWindow")
            .data("kendoWindow")
            .refresh({
                url: popup.editUrl,
                data: {
                    id: $(e.target).closest("a").attr("data-id")
                }
            })
            .center()
            .open();

        event = e;
    };

    popup.editClickDetail = function (e) {
        e.preventDefault();
        $(".k-window-title").html(popup.editTitle);
        $("#addEditWindow")
            .data("kendoWindow")
            .refresh({
                url: popup.editDetailUrl,
                data: {
                    id: $(e.target).closest("a").attr("data-id")
                }
            })
            .center()
            .open();

        event = e;
    };

    popup.editClickDetail1 = function (e) {
        e.preventDefault();
        $(".k-window-title").html(popup.editTitle);
        $("#addEditWindow")
            .data("kendoWindow")
            .refresh({
                url: popup.editDetail1Url,
                data: {
                    id: $(e.target).closest("a").attr("data-id")
                }
            })
            .center()
            .open();

        event = e;
    };

    popup.closeClick = function (e) {
        e.preventDefault();
        $("#addEditWindow").data("kendoWindow").close();
    };

    popup.closeClickDetail = function (e) {
        e.preventDefault();
        $("#addEditWindowDetail").data("kendoWindow").close();
    };

    popup.deleteClick = function (e) {
        e.preventDefault();
        var data = [];
        var selectedRow = $(e.currentTarget.parentElement.parentElement).find("#list-check > option");
        if (selectedRow.length > 0) {
            selectedRow.each(function () {
                data.push($(this).val());
            });
        } else {
            selectedRow = $("input[name='chkDelete']:checked");
            selectedRow.each(function () {
                data.push($(this).val());
            });
        }

        if (data.length > 0) {
            var length = data.length;
            var allItemLength = $(e.currentTarget.parentElement.parentElement).find("#list-all-item > option").length;
            var rowText = length === 1 ? " row" : " rows";
            if (length === allItemLength) {
                length = "all";
                rowText = " rows";
            }
            var text = "You have selected <b>" + length + rowText + "</b> for deletion<br>WARNING: This operation cannot be undone!";
            swal({
                title: "Are you sure?",
                text: text,
                showCancelButton: true,
                confirmButtonColor: "#c0392b",
                confirmButtonText: "Yes, delete it!",
                imageUrl: "/Content/Images/delete.png",
                closeOnConfirm: false,
                showLoaderOnConfirm: true,
                html: true
            },
                function (isConfirm) {
                    if (isConfirm) {
                        e.preventDefault();

                        $("#btnDelete").disableBtn();
                        $.ajax({
                            type: "POST",
                            url: popup.deleteUrl,
                            data: {
                                arrayOfID: data
                            },
                            traditional: true,
                            success: function (result) {
                                $("#btnDelete").enableBtn();
                                if (result.Message != null) {
                                    if (result.IsSuccess) {
                                        Chaka.setEvent(e);
                                        Chaka.refreshGrid();
                                        if ($("#grid-registered").length) {
                                            Chaka.refreshGrid("grid-registered");
                                        }
                                        if ($("#grid-unregistered").length) {
                                            Chaka.refreshGrid("grid-unregistered");
                                        }
                                        swal(result.Message, "Click ok to proceed", "success");
                                    } else {
                                        $("#btnDelete").enableBtn();
                                        swal({
                                            title: "Something Wrong",
                                            text: result.Message,
                                            type: "error"
                                        });
                                    }
                                }
                            },
                            error: function (result) {
                                $("#btnDelete").enableBtn();
                                swal({
                                    title: "Error",
                                    text: result.Message,
                                    type: "error"
                                });
                            }
                        });
                    }
                });
        } else {
            swal({
                title: "Failed",
                text: "There was an error running your action. Please select one or more items!",
                type: "error"
            });
        }
    };

    popup.deleteRowClick = function (e) {
        e.preventDefault();
        var text = "";
        swal({
            title: "Are you sure?",
            text: text,
            showCancelButton: true,
            confirmButtonColor: "#c0392b",
            confirmButtonText: "Yes, delete it!",
            imageUrl: "/Images/delete.png",
            closeOnConfirm: false,
            showLoaderOnConfirm: true,
            html: true
        },
            function (isConfirm) {
                if (isConfirm) {
                    e.preventDefault();
                    $.ajax({
                        type: "POST",
                        url: popup.deleteRowUrl,
                        data: {
                            id: $(e.target).closest("a").attr("data-id")
                        },
                        traditional: true,
                        success: function (result) {
                            if (result.Message != null) {
                                if (result.IsSuccess) {
                                    Chaka.setEvent(e);
                                    Chaka.refreshGrid();
                                    if ($("#grid-registered").length) {
                                        Chaka.refreshGrid("grid-registered");
                                    }
                                    if ($("#grid-unregistered").length) {
                                        Chaka.refreshGrid("grid-unregistered");
                                    }
                                    swal(result.Message, "Click ok to proceed", "success");
                                } else {
                                    swal({
                                        title: "Something Wrong",
                                        text: result.Message,
                                        type: "error"
                                    });
                                }
                            }
                        },
                        error: function (result) {
                            swal({
                                title: "Error",
                                text: result.Message,
                                type: "error"
                            });
                        }
                    });
                }
            });
    };

    popup.saveClick = function (e) {
        e.preventDefault();
        const validator = $("#addEditForm").kendoValidator(Chaka.validatorOptions).data("kendoValidator");
        if (validator.validate()) {
            $("#btnSave").disableBtn();
            $.ajax({
                type: "POST",
                url: $("#ID").val() == "" ? popup.createUrl : popup.editUrl,
                data: $("#addEditForm").serialize(),
                success: function (result) {
                    $("#addEditWindow").data("kendoWindow").close();

                    if (result.Message != null) {
                        if (result.IsSuccess) {
                            Chaka.setEvent(event);
                            Chaka.refreshGrid();
                            if ($("#grid-registered").length) {
                                Chaka.refreshGrid("grid-registered");
                            }
                            if ($("#grid-unregistered").length) {
                                Chaka.refreshGrid("grid-unregistered");
                            }

                            swal(result.Message, "Click ok to proceed!", "success");
                        } else {
                            $("#btnSave").enableBtn();
                            swal({
                                title: "Something Wrong",
                                text: result.Message,
                                type: "error",
                                html: true
                            });
                        }
                    }
                },
                error: function (err) {
                    $("#btnSave").enableBtn();
                    swal({
                        title: "Error",
                        text: err.Message,
                        type: "error",
                        html: true
                    });
                }
            });
        }
    };


    popup.saveClickDetail = function (e) {
        e.preventDefault();
        const validator = $("#addEditFormDetail").kendoValidator(Chaka.validatorOptions).data("kendoValidator");
        if (validator.validate()) {
            $("#btnSaveDetail").disableBtn();
            $.ajax({
                type: "POST",
                url: $("#ID").val() == "" ? popup.createDetailUrl : popup.editDetailUrl,
                data: $("#addEditFormDetail").serialize(),
                success: function (result) {
                    $("#addEditWindowDetail").data("kendoWindow").close();

                    if (result.Message != null) {
                        if (result.IsSuccess) {
                            Chaka.setEvent(event);
                            Chaka.refreshGrid();
                            if ($("#grid-registered").length) {
                                Chaka.refreshGrid("grid-registered");
                            }
                            if ($("#grid-unregistered").length) {
                                Chaka.refreshGrid("grid-unregistered");
                            }

                            swal(result.Message, "Click ok to proceed!", "success");
                        } else {
                            $("#btnSaveDetail").enableBtn();
                            swal({
                                title: "Something Wrong",
                                text: result.Message,
                                type: "error",
                                html: true
                            });
                        }
                    }
                },
                error: function (err) {
                    $("#btnSaveDetail").enableBtn();
                    swal({
                        title: "Error",
                        text: err.Message,
                        type: "error",
                        html: true
                    });
                }
            });
        }
    };


    popup.createDetailClick = function (e) {
        e.preventDefault();
        $(".k-window-title").html(popup.createTitle);
        $("#addEditWindow")
            .data("kendoWindow")
            .refresh({
                url: popup.createDetailUrl,
                data: {
                    headerID: $(e.target).closest("a").attr("data-id")
                }
            })
            .center()
            .open();

        event = e;
    };

    popup.createDetail1Click = function (e) {
        e.preventDefault();
        $(".k-window-title").html(popup.createTitle);
        $("#addEditWindowDetail")
            .data("kendoWindow")
            .refresh({
                url: popup.createDetail1Url,
                data: {
                    headerID: $(e.target).closest("a").attr("data-id")
                }
            })
            .center()
            .open();

        event = e;
    };

    return popup;

})(popup || {});

$(function () {
    $("#addEditWindow").kendoWindow(popup.getWindowOptions());
    $("#addEditWindowDetail").kendoWindow(popup.getWindowOptionsDetail());

    $("#btnAddNew").click(popup.addNewClick);
    $("#btnDelete").click(popup.deleteClick);
    $("#btnContinueDelete").click(popup.continueDeleteClick);
    $("#grid").on("click", ".editRow", popup.editClick);
    $("#grid").on("click", ".editRowDetail", popup.editClickDetail);
    $("#grid").on("click", ".editRowDetail1", popup.editClickDetail1);

    $("#grid").on("click", ".deleteRow", popup.deleteRowClick);
    $("#btnAddNewDetail").click(popup.createDetailClick);
    $("#btnAddNewDetail1").click(popup.createDetail1Click);
    $("#publishWindow").kendoWindow(popup.getWindowOptions());
    $("#publishWindowDetail").kendoWindow(popup.getWindowOptionsDetail());

    $("#btnPublish").click(popup.publishClick);
});