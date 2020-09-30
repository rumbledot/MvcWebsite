function addNewStiky(id) {
    var StikyText = $('#newStiky').val();
    var form = $('#__AjaxAntiForgeryForm');
    var token = $('input[name="__RequestVerificationToken"]', form).val();

    if (StikyText || StikyText.length > 4) {
        $.ajax({
            url: "/Boards/NewStiky",
            headers: {
                "RequestVerificationToken": token
            },
            type: "POST",
            data: JSON.stringify({
                Text: StikyText,
                BoardId: id
            }),
            dataType: 'JSON',
            async: true,
            contentType: 'application/json; charset=UTF-8',
            error: function (xhRequest, ErrorText, thrownError) {
                console.log('OH NOOOO');
            }
        }).done((response) => {
            updateStikies(response);
        });

    } else {
        $('#newStiky').attr('placeholder', 'Stiky can\'t be empty');
    }

    $('#newStiky').val('');
}

function updateStikies(stikies) {
    var stikies_container = $('#container-stikies');
    stikies_container.empty();
    $.each(stikies, function (i, s) {
        var stiky = '<div class="col-8" stye="word-wrap:break-word"><p>' + s.text + '</p></div>';
        stikies_container.append(stiky);
    });
}

// textarea word counter
$("#newStiky").on('keyup', function () {
    var words = $("#newStiky").val().length;
    var left = 255 - words;

    $('#wordCounter').text(left);
});

// List tabs
$('a[data-toggle="list"]').on('shown.bs.tab', function (e) {
    e.target // newly activated tab
    e.relatedTarget // previous active tab
})

// Stiky Image previewer
$("#newStikyPic").change(function () {
    var data = new FormData();
    var files = $("#newStikyPic").get(0).files;
    var form = $('#__AjaxAntiForgeryForm');
    var token = $('input[name="__RequestVerificationToken"]', form).val();

    checkfile();
    previewPic(this)

    if (files.length > 0) {
        data.append("newStikyPic", files[0]);
    }
});

function previewPic(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $('#newStikyPicPreview').attr('src', e.target.result);
        }

        reader.readAsDataURL(input.files[0]);
    }
}

// FILE UPLOAD FUNCTIONS

//get file size
function GetFileSize(fileid) {
    try {
        var fileSize = 0;
        //for IE
        if ($.browser.msie) {
            //before making an object of ActiveXObject,
            //please make sure ActiveX is enabled in your IE browser
            var objFSO = new ActiveXObject("Scripting.FileSystemObject"); var filePath = $("#" + fileid)[0].value;
            var objFile = objFSO.getFile(filePath);
            var fileSize = objFile.size; //size in kb
            fileSize = fileSize / 1048576; //size in mb
        }
        //for FF, Safari, Opeara and Others
        else {
            fileSize = $("#" + fileid)[0].files[0].size //size in kb
            fileSize = fileSize / 1048576; //size in mb
        }
        return fileSize;
    }
    catch (e) {
        alert("Error is :" + e);
    }
}

//get file path from client system
function getNameFromPath(strFilepath) {
    var objRE = new RegExp(/([^\/\\]+)$/);
    var strName = objRE.exec(strFilepath);

    if (strName == null) {
        return null;
    }
    else {
        return strName[0];
    }
}

function checkfile() {
    var file = getNameFromPath($("#newStikyPic").val());
    if (file != null) {
        var extension = file.substr((file.lastIndexOf('.') + 1));
        // alert(extension);
        switch (extension) {
            case 'jpg':
            case 'png':
            case 'gif':
            case 'jpeg':
                flag = true;
                break;
            default:
                flag = false;
        }
    }
    if (flag == false) {
        $("#spanfile").text("You can upload only jpg,png,gif,pdf extension file");
        return false;
    }
    else {
        var size = GetFileSize('newStikyPic');
        if (size > 3) {
            $("#spanfile").text("You can upload file up to 3 MB");
            return false;
        }
        else {
            $("#spanfile").text("");
        }
    }
}