/**
 * Downloads file from the specified url.
 * @param {any} url
 * @param {any} options
 */
downloadFile = function (url, options) {
    fetch(url, options).then(response => {
        let reader = response.body.getReader();
        let stream = new ReadableStream({
            start(controller) {
                function pushData() {
                    reader.read().then(({ done, value }) => {
                        if (done) {
                            controller.close();
                            return;
                        }
                        controller.enqueue(value);
                        pushData();
                    });
                }

                pushData();
            }
        });

        return {
            stream: stream,
            contentDisposition: response.headers.get("Content-Disposition")
        }
    }).then(function (responseData) {
        return {
            blob: new Response(responseData.stream).blob(),
            fileName: getFileName(responseData.contentDisposition)
        };
    }).then(function (resp) {
        resp.blob.then(function (blob) {

            if (blob.size > 0) {
                if (!resp.fileName) {
                    resp.fileName = new Date().toLocaleDateString();
                }

                var blobUrl = URL.createObjectURL(blob);
                var link = document.createElement("a");
                link.href = blobUrl;
                link.download = resp.fileName;
                link.click();
            }
            else {
                toastr.error("A aparut o eroare la descarcarea fisierului.");
            }
        });
    });

}

/**
 * Extracts file Name from contentDisposition.
 * @param {any} contentDisposition
 */
const getFileName = function (contentDisposition) {
    var filename = "";
    if (contentDisposition && contentDisposition.indexOf('attachment') !== -1) {
        var filenameRegex = /filename[^;=\n]*=((['"]).*?\2|[^;\n]*)/;
        var matches = filenameRegex.exec(contentDisposition);
        if (matches != null && matches[1]) {
            filename = matches[1].replace(/['"]/g, '');
        }
    }

    return filename;
}