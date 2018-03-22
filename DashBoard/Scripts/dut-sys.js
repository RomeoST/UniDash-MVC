function regComplete(data) {
    $('.parsley-required').remove();
    $('.parsley-error').removeClass('parsley-error');

    var list = data.responseJSON;

    if (typeof data.responseJSON !=='undefined' && list.model == 'confirmed') {
        $("#errorList").after("<p class='parsley-required' style='color:green;'>Ваш аккаунт зареєстровано</p><p>Ви будете перенаправлені через 5 сек.</p>");
        new PNotify({
            title: 'Реєстрація завершена',
            text: 'Перенаправлення...',
            type: 'success',
            styling: 'bootstrap3'
        });
        setTimeout(function () { location.href = location.origin + data.responseJSON.href }, 5000);
        return;
    }


    if (typeof data.responseJSON === 'undefined') {
        list.forEach(function (item, i, list) {
            $('#' + item.key).after(function (indx) {
                $(this).css('margin', '0 0 5px');
                $(this).addClass('parsley-error');
                return "<p class='parsley-required' style='color:red;'>" + item.errors[0] + "</p>";
            });
        });
    } else {
        list.modelList.forEach(function (item, i, list) {
            $("#errorList").after("<p class='parsley-required' style='color:red;'>" + item + "</p>");
        });
    }
    new PNotify({ title: 'Помилка', text: 'Форма реєстрації заповнена не вірно!', type: 'error', styling: 'bootstrap3' });
}

function loginComplete(data) {
    $('.parsley-required').remove();
    $('.parsley-error').removeClass('parsley-error');
    var list = data.responseJSON;
    if (typeof data.responseJSON === 'undefined') {
        new PNotify({ title: 'Помилка', text: 'Не всі поля заповнені', type: 'error', styling: 'bootstrap3' });        
    }else if (data.responseJSON.model === "confirmed") {
        setTimeout(function() { window.location.href = location.origin + data.responseJSON.href; }, 3000);
        new PNotify({title: 'Вхід виповнено',text: 'Перенаправлення...', type: 'success', styling: 'bootstrap3' });
    } else {
        list.modelList.forEach(function (item, i, list) {
            $("#errorList2").after("<p class='parsley-required' style='color:red;'>" + item + "</p>");
        });
        new PNotify({title: 'Помилка',text: 'Не точність даних',type: 'error',styling: 'bootstrap3'});
    }

}

function requestNotifyComplete(data) {
    var list = typeof data.responseJSON !=='undefined' ? data.responseJSON : data;

    if (typeof list.model === "undefined") {
        new PNotify({
            title: 'Помилка',
            text: 'Деякі дані заповнені невірно',
            type: 'error',
            styling: 'bootstrap3'
        });
        return;
    }

    new PNotify({
        title: 'Результат',
        text: list.message,
        type: list.model=='confirmed'?'success':'error',
        styling: 'bootstrap3'
    });
}