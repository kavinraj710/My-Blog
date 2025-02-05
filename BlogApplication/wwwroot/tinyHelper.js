window.initTinyMCE = () => {
    console.log("Initializing TinyMCE...");

    // Destroy any existing TinyMCE instance first
    if (typeof tinymce !== "undefined" && tinymce.editors.length > 0) {
        console.log("Destroying previous TinyMCE instance...");
        tinymce.remove();
    }

    // Small delay ensures the textarea exists in the DOM
    setTimeout(() => {
        tinymce.init({
            selector: "#mytextarea",
            height: 500,
            menubar: true,
            plugins: [
                "advlist autolink lists link image charmap print preview anchor",
                "searchreplace visualblocks code fullscreen",
                "insertdatetime media table paste code help wordcount"
            ],
            toolbar: "undo redo | formatselect | bold italic backcolor | alignleft aligncenter " +
                "alignright alignjustify | bullist numlist outdent indent | " +
                "removeformat | help | image",

            // Enable image upload from local storage
            images_upload_handler: function (blobInfo, success, failure) {
                let reader = new FileReader();
                reader.onload = function () {
                    success(reader.result); // Inserts image as base64
                };
                reader.onerror = function () {
                    failure("Image upload failed.");
                };
                reader.readAsDataURL(blobInfo.blob());
            },

            // File picker callback for custom image upload
            file_picker_callback: function (callback, value, meta) {
                if (meta.filetype === "image") {
                    let input = document.createElement("input");
                    input.setAttribute("type", "file");
                    input.setAttribute("accept", "image/*");

                    input.onchange = function () {
                        let file = this.files[0];
                        let reader = new FileReader();
                        reader.onload = function () {
                            callback(reader.result, { title: file.name });
                        };
                        reader.readAsDataURL(file);
                    };

                    input.click();
                }
            },

            setup: function (editor) {
                editor.on("init", function () {
                    console.log("TinyMCE initialized successfully.");
                });
            }
        });
    }, 100);
};

window.destroyTinyMCE = () => {
    console.log("Destroying TinyMCE...");
    if (typeof tinymce !== "undefined" && tinymce.editors.length > 0) {
        tinymce.remove();
    }
};
