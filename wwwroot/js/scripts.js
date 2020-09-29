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
        $('#newStiky').attr('placeholder','Stiky can\'t be empty');
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

$("#newStiky").on('keyup', function () {
    var words = $("#newStiky").val().length;
    var left = 255 - words;

    $('#wordCounter').text(left);
});