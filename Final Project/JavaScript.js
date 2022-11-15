function checkValidation(elem) {
    var path = elem.value;

    if (path.indexOf('.') == -1){
        return false;
    }

    var validExtent = new Array()
    var extent = path.substring(path.lastIndexOf('.') + 1).toLowerCase();

    validExtent[0] = "jpg";
    validExtent[1] = "jpeg";
    validExtent[2] = "bmp";
    validExtent[3] = "png";
    validExtent[4] = "tif";
    validExtent[5] = "tiff"; 

    for (var i = 0; i < validExtent.length; i++) {
        if (validExtent[i] == extent) {
            return true;
        }
    }

    alert("Unsuitable file extention. Please try again")
    document.getElementById("btn_courts").disabled = true
    return false;
}

