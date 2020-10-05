// List tabs
$('a[data-toggle="list"]').on('shown.bs.tab', function (e) {
    e.target // newly activated tab
    e.relatedTarget // previous active tab
})

// AJAX Functions
// add new Stiky to database
function addNewStiky(id, type, StikyText) {
    var form = $('#__AjaxAntiForgeryForm');
    var token = $('input[name="__RequestVerificationToken"]', form).val();

    if (StikyText || StikyText.length > 4) {
        var stiky = {};
        stiky.Type = type;
        stiky.Text = StikyText;
        stiky.BoardId = id;

        $.ajax({
            url: '/Boards/NewStiky',
            type: 'POST',
            headers: {
                'RequestVerificationToken': token
            },
            dataType: 'json',
            data: JSON.stringify(stiky),
            async: true,
            contentType: 'application/json',
            error: function (xhRequest, ErrorText, thrownError) {
                console.log('OH NOOOO');
            }
        }).done(() => {
            updateStikies(id);
        });
    } else {
        $('#newStiky').attr('placeholder', 'Stiky can\'t be empty');
    }

    $('#newStiky').val('');
}

// get Stikies from database
function updateStikies(id) {
    $.ajax({
        url: "/Boards/GetStikies",
        type: "GET",
        data: {
            Id: id
        },
        dataType: 'JSON',
        async: true,
        contentType: 'application/json; charset=UTF-8',
        error: function (xhRequest, ErrorText, thrownError) {
            console.log('OH NOOOO');
        }
    }).done((response) => {
        $('#container-stikies').empty();
        $.each(response, function (i, s) {
            var text = s.text;
            switch (s.type) {
                case "text":
                    var stiky = '<div class="col-8 justify-content-center" stye="word-wrap:break-word"><p>' + text + '</p></div>';
                    console.log('text', s.text)
                    break;
                case "image":
                    var stiky = '<div class="col-8 justify-content-center"><img class="lg-image" src="' + text + '" alt="' + s.text + '"/></div>';
                    console.log('image', s.text)
                    break;
                case "list":
                    break;
                default:
                    break;
            }
            stiky += '<div class="col-8 justify-content-center"><h3>*</h3></div>';
            $('#container-stikies').append(stiky);
        });
    });
}

// New STIKY TEXT
// Button event Upload
$('#btnAddStikyText').on('click', (e) => {
    var StikyText = $('#newStiky').val();
    var id = $('#btnAddStikyText').attr('data-boardid');

    addNewStiky(id, 'text', StikyText);
});

// textarea word counter
$("#newStiky").on('keyup', function () {
    var words = $("#newStiky").val().length;
    var left = 255 - words;

    $('#wordCounter').text(left);
});

// New STIKY IMAGE
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

// FILE UPLOAD FUNCTIONS
// AJAX Upload
$('#btnUploadStikyPic').on('click', (e) => {
    var fileUpload = $("#newStikyPic").get(0);
    var files = fileUpload.files;

    var data = new FormData();
    data.append('newStikyPic', files[0]);

    $.ajax({
        url: "/Boards/NewStikyImage",
        type: "POST",
        data: data,
        contentType: false,
        processData: false,
        error: function (err) {
            alert("ERROR : " + err.statusText);
        }
    }).done((response) => {
        var id = $('#btnUploadStikyPic').attr('data-boardid');
        console.log('ID : ' + id);
        console.log('res : ' + response);
        addNewStiky(id, 'image', response);
    });
    e.preventDefault();
});

//get file size
function GetFileSize(fileid) {
    try {
        var fileSize = 0;
        //for IE
        if (navigator.userAgent.search("MSIE") >= 0) {
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

function previewPic(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $('#newStikyPicPreview').attr('src', e.target.result);
        }

        reader.readAsDataURL(input.files[0]);
    }
}
