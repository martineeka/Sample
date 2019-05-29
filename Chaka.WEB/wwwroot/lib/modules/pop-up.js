var popup = (function(popup) {
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

    popup.getWindowOptions = function() {
        return windowOptions;
    };

    popup.addNewClick = function(e) {
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

    popup.editClick = function(e) {
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

    popup.editClickAttendanceCorrection = function (e) {
        e.preventDefault();
        var key = $(e.target).closest("a").attr("data-id");
        var json = localStorage.getItem(key);
        if (json!== null) {
            $(".k-window-title").html(popup.editTitle);
            var dialog = $("#addEditWindow")
                .data("kendoWindow")
                .refresh({
                    async: true,
                    url: popup.editUrl,
                    data: {
                        model: json
                    }
                })
                .center()
                .open();

            event = e;
        }
    };

    popup.closeClick = function(e) {
        e.preventDefault();
        $("#addEditWindow").data("kendoWindow").close();
    };

    popup.deleteClick = function(e) {
        e.preventDefault();
        var data = [];
        var selectedRow = $(e.currentTarget.parentElement.parentElement).find("#list-check > option");
        if (selectedRow.length > 0) {
            selectedRow.each(function() {
                data.push($(this).val());
            });
        } else {
            selectedRow = $("input[name='chkDelete']:checked");
            selectedRow.each(function() {
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
                function(isConfirm) {
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
                            success: function(result) {
                                $("#btnDelete").enableBtn();
                                if (result.Message != null) {
                                    if (result.IsSuccess) {
                                        Starbridges.setEvent(e);
                                        Starbridges.refreshGrid();
                                        if ($("#grid-registered").length) {
                                            Starbridges.refreshGrid("grid-registered");
                                        }
                                        if ($("#grid-unregistered").length) {
                                            Starbridges.refreshGrid("grid-unregistered");
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
                            error: function(result) {
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

    popup.saveClick = function(e) {
        e.preventDefault();
        const validator = $("#addEditForm").kendoValidator(Starbridges.validatorOptions).data("kendoValidator");
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
                            Starbridges.setEvent(event);
                            Starbridges.refreshGrid();
                            if ($("#grid-registered").length) {
                                Starbridges.refreshGrid("grid-registered");
                            }
                            if ($("#grid-unregistered").length) {
                                Starbridges.refreshGrid("grid-unregistered");
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
                error: function(err) {
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

    popup.essSaveClick = function(e) {
        e.preventDefault();
        const validator = $("#addEditForm").kendoValidator(Starbridges.validatorOptions).data("kendoValidator");
        if (validator.validate()) {
            $("#btnESSSave").disableBtn();
            $("#btnESSSubmit").hide();
            $.ajax({
                type: "POST",
                url: $("#ID").val() == ""
                    ? popup.createUrl + "?transactionStatus=Save"
                    : popup.editUrl + "?transactionStatus=Save",
                data: $("#addEditForm").serialize(),
                success: function (result) {
                    Starbridges.setEvent(event);
                    Starbridges.refreshGrid();
                    if ($("#grid-registered").length) {
                        Starbridges.refreshGrid("grid-registered");
                    }
                    if ($("#grid-unregistered").length) {
                        Starbridges.refreshGrid("grid-unregistered");
                    }
                    $("#addEditWindow").data("kendoWindow").close();
                    if (result.Message != null) {
                        if (result.IsSuccess) {
                            swal(result.Message, "Click ok to proceed!", "success");
                        } else {
                            $("#btnESSSave").enableBtn();
                            $("#btnESSSubmit").show();
                            swal({
                                title: "Something Wrong",
                                text: result.Message,
                                type: "error",
                                html: true
                            });
                        }
                    }
                },
                error: function(err) {
                    $("#btnESSSave").enableBtn();
                    $("#btnESSSubmit").show();
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

    popup.essSubmitClick = function(e) {
        e.preventDefault();
        const validator = $("#addEditForm").kendoValidator(Starbridges.validatorOptions).data("kendoValidator");
        if (validator.validate()) {
            $("#btnESSSave").disableBtn();
            $("#btnESSSubmit").hide();
            $("#btnClose").hide();

            $.ajax({
                type: "POST",
                url: $("#ID").val() == ""
                    ? popup.createUrl + "?transactionStatus=Submit"
                    : popup.editUrl + "?transactionStatus=Submit",
                data: $("#addEditForm").serialize(),
                success: function (result) {
                    Starbridges.setEvent(event);
                    Starbridges.refreshGrid();
                    if ($("#grid-registered").length) {
                        Starbridges.refreshGrid("grid-registered");
                    }
                    if ($("#grid-unregistered").length) {
                        Starbridges.refreshGrid("grid-unregistered");
                    }
                    $("#addEditWindow").data("kendoWindow").close();
                    if (result.Message != null) {
                        if (result.IsSuccess) {
                            swal(result.Message, "Click ok to proceed!", "success");
                        } else {
                            $("#btnESSSave").enableBtn();
                            $("#btnESSSubmit").show();
                            swal({
                                title: "Something Wrong",
                                text: result.Message,
                                type: "error",
                                html: true
                            });
                        }
                    }
                },
                error: function(err) {
                    $("#btnESSSave").enableBtn();
                    $("#btnESSSubmit").show();
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

    popup.publishClick = function (e) {
        e.preventDefault();
        $(".k-window-title").html(popup.publishTitle);
        $("#publishWindow")
            .data("kendoWindow")
            .refresh({
                url: popup.publishUrl
            })
            .center()
            .open();

        event = e;
    };

    popup.AnnouncSaveClick = function (e) {
        e.preventDefault();
        const validator = $("#addEditForm").kendoValidator(Starbridges.validatorOptions).data("kendoValidator");
        if (validator.validate()) {
            $("#btnConfirm").disableBtn();
            $.ajax({
                type: "POST",
                url: $("#ID").val() == "" ? popup.createUrl : popup.editUrl,
                data: $("#addEditForm").serialize(),
                success: function (result) {
                    $("#publishWindow").data("kendoWindow").close();

                    if (result.Message != null) {
                        if (result.IsSuccess) {
                            Starbridges.setEvent(event);
                            Starbridges.refreshGrid();
                            if ($("#grid-registered").length) {
                                Starbridges.refreshGrid("grid-registered");
                            }
                            if ($("#grid-unregistered").length) {
                                Starbridges.refreshGrid("grid-unregistered");
                            }
                            swal(result.Message, "Click ok to proceed!", "success");
                        } else {
                            $("#btnConfirm").enableBtn();
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
                    $("#btnConfirm").enableBtn();
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

    return popup;

})(popup || {});

$(function() {
    $("#addEditWindow").kendoWindow(popup.getWindowOptions());
    $("#btnAddNew").click(popup.addNewClick);
    $("#btnDelete").click(popup.deleteClick);
    $("#btnContinueDelete").click(popup.continueDeleteClick);
    $("#grid").on("click", ".editRow", popup.editClick);
    $("#grid").on("click", ".editRowAttendance", popup.editClickAttendanceCorrection);
    $("#btnAddNewDetail").click(popup.createDetailClick);
    $("#publishWindow").kendoWindow(popup.getWindowOptions());
    $("#btnPublish").click(popup.publishClick);
});