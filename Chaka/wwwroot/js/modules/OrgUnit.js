popup.getUrl = function () {
    return popup.CreateDetailJobTitleUrl + "?headerID=" + $("#headerID").val();
}
popup.getUrlEdit = function () {
    return popup.EditDetailJobTitleUrl + "?headerID=" + $("#headerID").val();
}

popup.saveHeaderClick = function (e) {
    e.preventDefault();
    const validator = $("#addEditFormHeader").kendoValidator(Chaka.validatorOptions).data("kendoValidator");
    if (validator.validate()) {
        $("#btnAddNewDetailJobTitle").disableBtn();
        $.ajax({
            type: "POST",
            url: popup.createHeaderUrl,
            data: $("#addEditFormHeader").serialize(),
            success: function (result) {
                if (result.Message != null) {
                    if (result.IsSuccess) {
                        $("#OrganizationID").val(result.Message)
                        $("#btnAddNewDetailJobTitle").enableBtn();

                        popup.createUrl = popup.getUrl();

                        popup.addNewClick(e);

                    } else {
                        $("#btnAddNewDetailJobTitle").enableBtn();
                        swal({
                            title: "Something Wrong",
                            text: result.Message,
                            type: "error",
                            html: true
                        });
                    }
                } else {
                    swal({
                        title: "Something Wrong",
                        text: result.Message,
                        type: "error",
                        html: true
                    });
                }
            },
            error: function (err) {
                $("#btnAddNewDetailJobTitle").enableBtn();
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

popup.btnSaveHeaderClick = function (e) {
    e.preventDefault();
    const validator = $("#addEditFormHeader").kendoValidator(Chaka.validatorOptions).data("kendoValidator");
    if (validator.validate()) {
        $("#btnSaveHeader").disableBtn();
        $("#btnSubmitHeader").hide();
        $.ajax({
            type: "POST",
            url: $("#OrganizationID").val() == "0" ? popup.saveSave : popup.saveEdit,
            data: $("#addEditFormHeader").serialize(),
            success: function (result) {
                Chaka.setEvent(e);
                Chaka.refreshGrid();
                if ($("#grid-registered").length) {
                    Chaka.refreshGrid("grid-registered");
                }
                if ($("#grid-unregistered").length) {
                    Chaka.refreshGrid("grid-unregistered");
                }
                $("#addEditWindowDetail").data("kendoWindow").close();
                if (result.Message != null) {
                    if (result.IsSuccess) {
                        swal(result.Message, "Click ok to proceed!", "success");
                        $("#OrganizationID").val(result.Data);
                    } else {
                        $("#btnSaveHeader").enableBtn();
                        $("#btnSubmitHeader").show();
                        swal({
                            title: "Something Wrong",
                            text: result.Message,
                            type: "error",
                            html: true
                        });
                    }
                } else {
                    swal({
                        title: "Something Wrong",
                        text: result.Message,
                        type: "error",
                        html: true
                    });
                }
                $("#btnSaveHeader").enableBtn();
                $("#btnSubmitHeader").show();
                GetAmount();
            },
            error: function (err) {
                $("#btnSaveHeader").enableBtn();
                $("#btnSubmitHeader").show();
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

popup.btnSubmitHeaderClick = function (e) {
    e.preventDefault();
    const validator = $("#addEditFormHeader").kendoValidator(Chaka.validatorOptions).data("kendoValidator");
    if (validator.validate()) {
        $("#btnSaveHeader").hide();
        $("#btnSubmitHeader").disableBtn();
        $("#btnClose").hide();

        $.ajax({
            type: "POST",
            url: $("#OrganizationID").val() == "0" ? popup.submitSave : popup.submitEdit,
            data: $("#addEditFormHeader").serialize(),
            success: function (result) {
                Chaka.setEvent(e);
                Chaka.refreshGrid();
                if ($("#grid-registered").length) {
                    Starbridges.refreshGrid("grid-registered");
                }
                if ($("#grid-unregistered").length) {
                    Chaka.refreshGrid("grid-unregistered");
                }
                $("#addEditWindowDetail").data("kendoWindow").close();
                if (result.Message != null) {
                    if (result.IsSuccess) {
                        swal({
                            title: result.Message,
                            text: "Click ok to proceed!",
                            type: "success"
                        },
                            function () {
                                $("#Loading").show();
                                window.location.href = popup.UrlIndex;
                            });
                    } else {
                        //$("#btnSaveHeader").show();
                        $("#btnSubmitHeader").enableBtn();
                        swal({
                            title: "Something Wrong",
                            text: result.Message,
                            type: "error",
                            html: true
                        });
                    }
                } else {
                    swal({
                        title: "Something Wrong",
                        text: result.Message,
                        type: "error",
                        html: true
                    });
                }
            },
            error: function (err) {
                $("#btnSaveHeader").show();
                $("#btnSubmitHeader").enableBtn();
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


popup.addNewDetailClick = function (e) {
    id = $("#OrganizationID").val();
   
        if (id == 0) {
            popup.saveHeaderClick(e);
        } else {
            popup.createUrl = popup.getUrl();
            popup.addNewClick(e);
        }
        GetAmount();
    
};
popup.getUrlAndEdit = function (e) {
    popup.editUrl = popup.getUrlEdit();
    popup.editClick(e);
}

popup.SaveAndGetAmount = function (e) {
    e.preventDefault();
    popup.saveClick(e);
    GetAmount();
};

var rejectedData = [];

popup.rejectDetail = function (e) {
    e.preventDefault();
    var selectedRow = $(e.currentTarget.parentElement.parentElement).find("#list-check > option");
    if (selectedRow.length > 0) {
        rejectedData = [];
        selectedRow.each(function () {
            rejectedData.push($(this).val());
        });
    } else {
        selectedRow = $("input[name='chkDetail']:checked");
        rejectedData = [];
        selectedRow.each(function () {
            rejectedData.push($(this).val());
        });
    }
    if (rejectedData.length > 0) {
        var rejectItem = rejectedData.length;
        var newURL = "";
        var newURL = popup.rejectDetailUrl + "?totalRejectItem=" + rejectItem;
        $(".k-window-title").html("Reject Detail");
        var dialog = $("#rejectDetailWindow")
            .data("kendoWindow")
            .refresh({
                url: newURL,
            })
            .center()
            .open();
        event = e;
    }
}

popup.rejectClick = function (e) {
    e.preventDefault();
    const validator = $("#rejectDetailForm").kendoValidator(Starbridges.validatorOptions).data("kendoValidator");
    if (validator.validate()) {
        $("#btnReject").disableBtn();
        $("#btnCancelReject").hide();
        $.ajax({
            type: "POST",
            url: popup.rejectDetailUrl,
            data: {
                arrayOfID: rejectedData,
                approverNotes: $("#ApproverNotes").val()
            },
            traditional: true,
            success: function (result) {
                $("#btnRejectDetail").enableBtn();
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
                        $("#btnReject").enableBtn();
                        $("#btnCancelReject").show();
                        $("#rejectDetailWindow").data("kendoWindow").close();
                        rejectedData = [];
                        swal(result.Message, "Click ok to proceed", "success");
                    } else {
                        $("#btnReject").enableBtn();
                        $("#btnCancelReject").show();
                        $("#rejectDetailWindow").data("kendoWindow").close();
                        rejectedData = [];
                        swal({
                            title: "Something Wrong",
                            text: result.Message,
                            type: "error"
                        });
                    }
                }
                GetAmount();
            },
            error: function (result) {
                $("#btnReject").enableBtn();
                $("#btnCancelReject").show();
                $("#rejectDetailWindow").data("kendoWindow").close();
                swal({
                    title: "Error",
                    text: result.Message,
                    type: "error"
                });
            }
        });
    }
};

popup.undoRejectDetail = function (e) {
    e.preventDefault();
    var data = [];
    var selectedRow = $(e.currentTarget.parentElement.parentElement).find("#list-check > option");
    if (selectedRow.length > 0) {
        selectedRow.each(function () {
            data.push($(this).val());
        });
    } else {
        selectedRow = $("input[name='chkDetail']:checked");
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
        var text = "You have selected <b>" + length + rowText + "</b> to undo reject.<br>";
        swal({
            title: "Are you sure?",
            text: text,
            showCancelButton: true,
            confirmButtonColor: "#c0392b",
            confirmButtonText: "Yes",
            imageUrl: "/Content/Images/delete.png",
            closeOnConfirm: false,
            showLoaderOnConfirm: true,
            html: true
        },
            function (isConfirm) {
                if (isConfirm) {
                    e.preventDefault();

                    $("#btnUndoRejectDetail").disableBtn();
                    $.ajax({
                        type: "POST",
                        url: popup.undoRejectDetailUrl,
                        data: {
                            arrayOfID: data
                        },
                        traditional: true,
                        success: function (result) {
                            $("#btnUndoRejectDetail").enableBtn();
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
                                    $("#btnUndoRejectDetail").enableBtn();
                                    swal({
                                        title: "Something Wrong",
                                        text: result.Message,
                                        type: "error"
                                    });
                                }
                            }
                            GetAmount();
                        },
                        error: function (result) {
                            $("#btnUndoRejectDetail").enableBtn();
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

popup.deleteDetailAndGetAmount = function (e) {
    e.preventDefault();
    var data = [];
    var selectedRow = $(e.currentTarget.parentElement.parentElement).find("#list-check > option");
    if (selectedRow.length > 0) {
        selectedRow.each(function () {
            data.push($(this).val());
        });
    } else {
        selectedRow = $("input[name='chkDetail']:checked");
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
                            $("#btnDeleteDetail").enableBtn();
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
                                    $("#btnDeleteDetail").enableBtn();
                                    swal({
                                        title: "Something Wrong",
                                        text: result.Message,
                                        type: "error"
                                    });
                                }
                            }
                            GetAmount();
                        },
                        error: function (result) {
                            $("#btnDeleteDetail").enableBtn();
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

function GetAmount() {
    id = $("#OrganizationID").val();
    if (id != 0) {
        $.ajax({
            type: "GET",
            async: false,
            url: popup.getAmount,
            traditional: true,
            data: {
                OrganizationID: id,
            },
            success: function (result) {
                $("#Amount").data("kendoNumericTextBox").value(result);
            },
            error: function (err) {
            }
        });
    } else {
        $("#Amount").data("kendoNumericTextBox").value(0);
    }
}

$(function () {
    //filterData();
    $("#grid").data("kendoGrid").dataSource.read();
    $("#Amount").data("kendoNumericTextBox").readonly();
});

