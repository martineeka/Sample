var kendoValidationHelper = {
    errorTemplate: '<span class="chaka-validation">#=message#</span>',

    remoteValidation: function (input) {
        var remoteAttr = input.attr("data-val-remote-url");
        if (typeof remoteAttr === typeof undefined || remoteAttr === false) {
            return true;
        }

        var isInvalid = true;
        var errorMessage = "Message";
        var data = {};

        data[input.attr("name")] = input.val();

        var additionalFieldsAttr = input.attr("data-val-remote-additionalfields");
        if (additionalFieldsAttr != undefined) {
            var additionalFields = additionalFieldsAttr.split(",");
            $.each(additionalFields,
                function (index, arrayEl) {
                    data[arrayEl.substring(2)] = $("#" + arrayEl.substring(2)).val();
                });
        }

        $.ajax({
            url: remoteAttr,
            mode: "abort",
            port: "validate" + input.attr("name"),
            dataType: "json",
            type: input.attr("data-val-remote-type"),
            data: data,
            async: false,
            success: function (response) {
                if (response == true || response == false) {
                    isInvalid = response;
                } else {
                    errorMessage = response;
                }

            }
        });

        if (errorMessage != "Message") {
            return errorMessage;
        }
        return !isInvalid;
    },

    remoteValidationMessage: function (input) {
        return input.data("val-remote");
    }
};

var kendoWindowHelper = (function ($) {
    return {
        loadingTemplate: "<div class='k-loading-image'></div>"
    };
})();

var Chaka = (function () {
    var event;

    function setEvent(e) {
        event = e;
    }

    function refreshGrid(gridId) {
        try {
            var rows;
            var page;
            $(event.currentTarget).parent().find("#checked-info-text").fadeOut("fast");
            setTimeout(function () {
                $(event.currentTarget).parent().find("#checked-info-text").html("<kbd><i class='fa fa-refresh fa-spin'></i> Working...</kbd>");
                $(event.currentTarget).parent().find("#checked-info-text").fadeIn("slow");
            }, 200);

            disabled($(event.currentTarget).parent().find("#checked-refresh"));
            disabled($(event.currentTarget).parent().find("#checked-select"));
            disabled($(event.currentTarget).parent().find("#checked-clear"));
            clearPool();
            if (gridId === undefined) {
                $("#grid").data("kendoGrid").dataSource.fetch(function () {
                    rows = $("#grid").data("kendoGrid").dataSource.view().length;
                    page = $("#grid").data("kendoGrid").dataSource.page();
                    if (rows === 0 && page > 1) {
                        $("#grid").data("kendoGrid").dataSource.page(page - 1);
                    }
                });

            } else {
                $("#" + gridId).data("kendoGrid").dataSource.fetch(function () {
                    rows = $("#" + gridId).data("kendoGrid").dataSource.view().length;
                    page = $("#" + gridId).data("kendoGrid").dataSource.page();
                    if (rows === 0 && page > 1) {
                        $("#" + gridId).data("kendoGrid").dataSource.page(page - 1);
                    }
                });
            }
        } catch (e) {
            // ignore
        }
    }

    function disabled(param) {
        $(param).attr("disabled", true);
        $(param).css("pointer-events", "none");
    }

    function clearPool() {
        if (event != undefined) {
            try {
                $(event.currentTarget).parents(".general-table-form").find("#list-all-item").empty();
            } catch (e) {
                // ignore
            }
        }
    }

    return {
        loadingTemplate: kendoWindowHelper.loadingTemplate,
        validationHelper: kendoValidationHelper.validationHelper,
        validatorOptions: {
            errorTemplate: kendoValidationHelper.errorTemplate,
            rules: {
                remote: kendoValidationHelper.remoteValidation,
            },
            messages: {
                remote: kendoValidationHelper.remoteValidationMessage
            }
        },
        refreshGrid: refreshGrid,
        setEvent: setEvent
    };
})();