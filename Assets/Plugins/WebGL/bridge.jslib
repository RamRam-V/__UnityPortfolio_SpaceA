mergeInto(LibraryManager.library, {
    OpenImageDialog: function () {
        try {
            window.dispatchReactUnityEvent("OpenImageDialog");
        } catch (e) {
            console.warn("Failed to dispatch event");
        }
    },
    OpenVideoDialog: function () {
        try {
            window.dispatchReactUnityEvent("OpenVideoDialog");
        } catch (e) {
            console.warn("Failed to dispatch event");
        }
    },
    OpenPdfDialog: function () {
        try {
            window.dispatchReactUnityEvent("OpenPdfDialog");
        } catch (e) {
            console.warn("Failed to dispatch event");
        }
    },

    HandleLogin: function (id, pw) {
        try {
            window.dispatchReactUnityEvent(
                "handleLogin",
                UTF8ToString(id),
                UTF8ToString(pw)
            );
        } catch (e) {
            console.warn("Failed to dispatch event");
        }
    },
});
