function LoadDrink(id) {
    $.ajax({
        type: "GET",
        url: "Admin/DrinkItem?key=" + key,
        data: { id: id },
        dataType: "html",
        success: function (data) {
            $("#drinkItemPanel").html(data);
        }
    })
}

function SaveDrink(form) {

    $.ajax({
        type: $(form).attr('method'),
        url: $(form).attr('action') + "?key=" + key,
        data: new FormData(form),
        dataType: "html",
        processData: false,
        contentType: false,
        success: function (data) {
            $("#drinkItemPanel").html(data);
            RefreshDrinkList();
        }
    });

    return false;
}

function DeleteDrink(id) {
    $.ajax({
        type: "POST",
        url: "Admin/DeleteDrink?key=" + key,
        data: { id: id },
        dataType: "html",
        success: function (data) {
            RefreshDrinkList();
            $("#drinkItemPanel").html(data);
        }
    });
}

function AddDrink() {
    LoadDrink(0);
}

function RefreshDrinkList() {
    $.ajax({
        type: "GET",
        url: "Admin/DrinkList?key=" + key,
        success: function (data) {
            $('#drinkTablePanel').html(data);
        }
    });
}

function RefreshCoinList() {
    $.ajax({
        type: "GET",
        url: "Admin/CoinList?key=" + key,
        success: function (data) {
            $('#coinTablePanel').html(data);
        }
    });
}

function ChangeBlockCoin(id) {
    $.ajax({
        type: "POST",
        url: "Admin/ChangeBlockCoin?key=" + key,
        data: { id: id },
        success: function (data) {
            RefreshCoinList();
        }
    });
}