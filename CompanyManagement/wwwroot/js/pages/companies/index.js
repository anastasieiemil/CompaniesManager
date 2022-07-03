"use strict";


const companiesController = function () {
    const context = {
        urls: {
            values: "/Companies/Values",
            loadFile: "/Companies/LoadFile",
            getCSV: "/Companies/GetCSV",
        },
        table: $("#companies_table"),
        modal: $("#load_file_modal"),
        loadBtn: $("#load_btn"),
        exportCSVBtn: $("#export_csv_btn"),
        selectors: {
            dropzone: "#file_dropzone"
        },
        fileType: $("#file_type"),
        antiForgeryToken: $("#antiforg")[0]["__RequestVerificationToken"].value,
        status:
        {
            success: 0,
            error: 1
        }
    }

    /**
     * Manages all logic for uploading a file.
     * */
    const dropzoneController = function () {
        var dropzone = null;

        return {
            init: function (successCallBack) {
                if (!dropzone) {
                    dropzone = new Dropzone(context.selectors.dropzone, {
                        url: context.urls.loadFile,
                        autoProcessQueue: false,
                        addRemoveLinks: true,
                        headers: { "X-CSRF": context.antiForgeryToken },
                        maxFiles: 1,
                        acceptedFiles: ".txt,.csv",
                        maxFilesize: 20, //MB
                        sending: function (file, xhr, formData) {
                            formData.append("FileType", context.fileType.val());
                        }
                    });

                    // Register success event.
                    dropzone.on("success", function (file, data) {

                        if (data.status == context.status.success) {
                            toastr.success(data.message, "Success!");
                        }
                        else if (data.status == context.status.error) {
                            toastr.error(data.message, "An error has occured.");
                        }
                        else {
                            toastr.error("Please contact our support team.", "An error has occured.");
                        }

                        if (successCallBack) {
                            successCallBack();
                        }

                    });


                    // Register error event.
                    dropzone.on("error", function (file, data) {
                        toastr.error("Please contact our support team.", "An error has occured.");
                    });
                }
            },
            reset: function () {
                dropzone.removeAllFiles(true);
            },
            validate: function () {
                if (dropzone.getAcceptedFiles().length == 1) {
                    return true;
                }
                else {
                    toastr.error("Please make sure that you have selected one valid file.", "Invalid or missing file!")
                    return false;
                }
            },
            load: function () {
                dropzone.processQueue();
            }
        }
    }();

    /**
     * Manages all logic for moidal.
     * */
    const modalController = function () {

        /**
         * Register action for on opening modal event.
         * */
        const registerEventForOpeningModal = function () {

            // Resets the dropzone.
            context.modal.on("show.bs.modal", function () {
                dropzoneController.reset();
            });
        }

        /**
         * Registers action for load event.
         * */
        const registerEventForLoadButton = function () {
            context.loadBtn.on("click", function () {
                if (dropzoneController.validate()) {
                    dropzoneController.load();
                }

            });
        }

        return {
            init: function () {
                registerEventForOpeningModal();
                registerEventForLoadButton();
            },
            closeModal: function () {
                context.modal.modal("hide");
            }
        }
    }();

    /**
     * Manages all logic for building the table.
     * */
    const tableEngine = function () {

        var table = null;

        return {
            init: function () {
                table = context.table.DataTable({
                    ajax: {
                        url: context.urls.values,
                        type: "POST",
                        headers: { "X-CSRF": context.antiForgeryToken }
                    },
                    serverSide: true,
                    searching: false,
                    ordering: true,
                    columns: [
                        {
                            data: "companyName",
                            searchable: false,
                        },
                        {
                            searchable: false,
                            data: "yearInBusiness"
                        },
                        {
                            searchable: false,
                            data: "contactName"
                        },
                        {
                            searchable: false,
                            data: "contactPhone"
                        },
                        {
                            searchable: false,
                            data: "contactEmail",
                            orderable: false
                        },
                    ]

                });
            },
            refresh: function () {
                table.ajax.reload(null, false);
            },
            getParams: function () {
                return table.ajax.params();
            }
        }
    }();

    /**
     * Register action for fetching data in csv format.
     * */
    const registerEventForExportingCSV = function () {
        context.exportCSVBtn.on("click", function () {
            event.preventDefault();
            event.stopPropagation();

            downloadFile(context.urls.getCSV, {
                body: JSON.stringify(tableEngine.getParams()),
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    "X-CSRF": context.antiForgeryToken
                }
            });
        });
    }

    return {
        init: function () {

            tableEngine.init();
            modalController.init();
            dropzoneController.init(function () {
                modalController.closeModal();
                tableEngine.refresh();
            });
            registerEventForExportingCSV();
        }
    }
}();


$(document).ready(function () {

    companiesController.init();
})