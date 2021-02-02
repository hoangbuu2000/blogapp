$.validator.unobtrusive.adapters.addSingleVal("maxlength", "length");
$.validator.unobtrusive.adapters.addSingleVal("nospace", "nospace");

$.validator.addMethod("maxlength", (value, element, maxlength) => {
    if (value) {
        if (value.length > maxlength) {
            return false;
        }
    }
    return true;
});

$.validator.addMethod("nospace", (value, element, nospace) => {
    if (value) {
        if (value.indexOf(' ') >= 0) {
            return false;
        }
    }
    return true;
});