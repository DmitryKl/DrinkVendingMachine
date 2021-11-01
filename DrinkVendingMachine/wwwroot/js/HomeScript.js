function Buy(id) {
    $.ajax({
        type: "POST",
        url: "Home/Buy",
        data: { id: id },
        success: function () {
            ReduceCount(id);
        }
    });

    let cost = parseFloat($("#" + id).find("#cost").html());
    ChangeSum(-cost);
    DisableElements();
}

function ReduceCount(id) {
    let countElement = $("#" + id).find("span#count");
    let count = parseInt(countElement.html());
    count -= 1;
    countElement.html(count);

    DisableElements();
}

function InsertCoin(value) {
    ChangeSum(value);
    DisableElements();
}

function ChangeSum(value) {
    let sumElement = $("#sum");
    let sum = parseFloat(sumElement.html()) + value;
    sumElement.html(sum);
}

function TakeChange() {
    let sumElement = $("#sum");
    let changeElement = $("#change");

    value = parseInt(sumElement.html());

    $.ajax({
        type: "POST",
        url: "Home/TakeChange",
        data: { value: value },
        dataType: "html",
        success: function (data) {
            changeElement.html(data);
            sumElement.html(0);
            DisableElements();
        },
        error: function(error){
            changeElement.html("Ошибка при подсчете сдачи");
        }
    });    
    
    
}

function DisableElements() {
    let sum = parseFloat($("#sum").html());
    $(".drinkElement").each(function () {
        let cost = parseFloat($(this).find("#cost").html());
        let count = parseInt($(this).find("#count").html());

        if (count > 0 && cost <= sum) {
            $(this).removeClass("disabled");
        } else {
            $(this).addClass("disabled");
        }
    });
}

function AddCoin(value) {
    $.ajax({
        type: "POST",
        url: "Home/AddCoin",
        data: { value: value },
        success: function (data) {
            InsertCoin(value);
        }
    });
}
