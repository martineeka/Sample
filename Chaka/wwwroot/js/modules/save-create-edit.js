var createEdit = (function (createEdit) {
    createEdit.saveClick = function (e) {
        e.preventDefault();
        const validator = $("#addEditForm").kendoValidator(Chaka.validatorOptions).data("kendoValidator");
        if (validator.validate()) {
            $("#btnSave").disableBtn();
            $.ajax({
                type: "POST",
                url: $("#ID").val() == "" ? createEdit.createUrl : createEdit.editUrl,
                data: $("#addEditForm").serialize(),
                success: function (result) {
                    $("#addEditWindow").data("kendoWindow").close();
                    $("#btnSave").enableBtn();
                    if (typeof result == "object") {
                        swal({
                            title: "Something Wrong",
                            text: result.Message,
                            type: "error",
                            html: true
                        });
                    }
                    else {
                        $("#" + createEdit.gridID).data('kendoGrid').dataSource.read();
                        message = "This transaction has been submitted, please wait for admin approval before your data is changed\n";
                        message = message + "Click ok!\n";
                        swal("Saved", message, "success");
                        $("#result").html(result);
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

    createEdit.saveDraft = function (e) {
        
        e.preventDefault();
        const validator = $("#addEditForm").kendoValidator(Chaka.validatorOptions).data("kendoValidator");
        if (validator.validate()) {
            $("#Loading").show();
            $("#SubmitType").val("Save");
            $("#btnSaveDraft").disableBtn();
            $.ajax({
                type: "POST",
                url: $("#ID").val() == "" ? createEdit.createUrl : createEdit.editUrl,
                data: $("#addEditForm").serialize(),
                success: function (result) {
                    $("#Loading").hide();
                    $("#btnSaveDraft").enableBtn();
                    if (typeof result == "object") {
                        swal({
                            title: "Something Wrong",
                            text: result.Message,
                            type: "error",
                            html: true
                        });
                    }
                    else {
                        swal("Saved", "Click ok to proceed!", "success");
                        $("#result").html(result);
                    }
                },
                error: function (err) {
                    $("#Loading").hide();
                    $("#btnSaveDraft").enableBtn();
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

    createEdit.saveSubmit = function (e) {
        e.preventDefault();
        const validator = $("#addEditForm").kendoValidator(Chaka.validatorOptions).data("kendoValidator");
        if (validator.validate()) {
            $("#Loading").show();
            $("#SubmitType").val("Submit");
            $("#btnSubmit").disableBtn();
            $.ajax({
                type: "POST",
                url: $("#ID").val() == "" ? createEdit.createUrl : createEdit.editUrl,
                data: $("#addEditForm").serialize(),
                success: function (result) {
                    $("#Loading").hide();
                    $("#btnSubmit").enableBtn();
                    if (typeof result == "object") {
                        swal({
                            title: "Something Wrong",
                            text: result.Message,
                            type: "error",
                            html: true
                        });
                    }
                    else {
                        swal("Submited", "Click ok to proceed!", "success");
                        $("#result").html(result);
                    }
                },
                error: function (err) {
                    $("#Loading").hide();
                    $("#btnSubmit").enableBtn();
                    swal({
                        title: "Error",
                        text: err.Message,
                        type: "error",
                        html: true
                    });
                }
            });
        }

    }


    return createEdit;
})(createEdit || {});

$(function () {
    $("#btnSave").click(createEdit.saveClick);
    $("#btnSaveDraft").click(createEdit.saveDraft);
    $("#btnSubmit").click(createEdit.saveSubmit);
});