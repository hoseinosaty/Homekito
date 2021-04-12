async function InitilizeElementArray(val, name) {
    var data = {};
    for (var i = 0; i < name.length; i++) {
        data[name[i]] = val[i];
    }
    return await IntilizeData(JSON.stringify(data));
}

async function AddToBasket() {
    let frmId = "frmData748";
    var enc = await InitilizeForm(frmId);
    $("#btnAddToCart").attr('disabled', 'disabled').css('pointer-event', 'none');
    await axios.post('/cart/AddToCart', {}, {
        headers: {
            'AddToCart': enc
        }
    }).then(function (res) {
        let data = res.data;
        console.info(res);
        $("#btnAddToCart").removeAttr('disabled').css('pointer-event', 'auto');
        if (data.status) {
            hrb_notify([
                'homekito',
                picProd,
                'fa-basket',
                "محصول مورد نظر با موفقیت به سبد خرید اضافه شد",
                'bottomLeft',
                'flipInY',
                'flipOutX',
                '5'
            ]);
            GetBasketAmount();
        }
        else {
            ErrorDialog(data.msg);
        }

    }).catch(function (err) {
        console.error(err);
        $("#btnAddToCart").removeAttr('disabled').css('pointer-event', 'auto');
    });

}
async function RmoveCartItem(pid) {
    debugger
     var enc = await InitilizeElementArray([pid], ["ProductCombineId"]);
     $("#removeItemBtn" + pid).attr('disabled', 'disabled').css('pointer-event', 'none');
     await axios.post('/cart/RemoveItem', {}, {
        headers: {
            'RemoveCartItem': enc
        }
    }).then(function (res) {
        let data = res.data;
        console.info(res);
        $("#removeItemBtn" + pid).removeAttr('disabled').css('pointer-event', 'auto');
        if (data.status) {
            window.location.href = window.location.href;
        }
        else {

        }

    }).catch(function (err) {
        console.error(err);
        $("#removeItemBtn" + pid).removeAttr('disabled').css('pointer-event', 'auto');
    });
}
async function IncreaseBasketItemQuantity(pid) {
    var count = parseInt($("#countProdInput").text());
    count++;

    var enc = await InitilizeElementArray([1, pid], ["Quantity", "ProductCombineId"]);
    await axios.post('/cart/IncreaseQuanity', {}, {
        headers: {
            'AddToCart': enc
        }
    }).then(function (res) {
        let data = res.data;
        console.info(res);
        if (data.status) {
            $("#countProdInput").html(count);
            LoadStep(1);
        }
        else {
            ErrorDialog(data.msg);
        }

    }).catch(function (err) {
        console.error(err);
    });
   
}
async function DecreaseBasketItemQuantity(pid) {
    var count = parseInt($("#countProdInput").text());
    count--;
    if (count < 1) {
        count = 1;
    }
    var enc = await InitilizeElementArray([1, pid], ["Quantity", "ProductCombineId"]);
    await axios.post('/cart/DecreaseQuanity', {}, {
        headers: {
            'AddToCart': enc
        }
    }).then(function (res) {
        let data = res.data;
        console.info(res);
        if (data.status) {
            $("#countProdInput").html(count);
            LoadStep(1);
        }
        else {
            ErrorDialog(data.msg);
        }

    }).catch(function (err) {
        console.error(err);
    });
}

async function AddToBasketManual() {
    let frmId = "frmDatamanual";
    var enc = await InitilizeForm(frmId);
    $("#btnAddToCartmanual").attr('disabled', 'disabled').css('pointer-event', 'none');
    await axios.post('/cart/AddToCart', {}, {
        headers: {
            'AddToCart': enc
        }
    }).then(function (res) {
        let data = res.data;
        console.info(res);
        $("#btnAddToCartmanual").removeAttr('disabled').css('pointer-event', 'auto');
        if (data.status) {
            hrb_notify([
                'homekito',
                picProd,
                'fa-basket',
                "محصول مورد نظر با موفقیت به سبد خرید اضافه شد",
                'bottomLeft',
                'flipInY',
                'flipOutX',
                '5'
            ]);
            //GetBasketAmount();
        }
        else {
            ErrorDialog(data.msg);
        }

    }).catch(function (err) {
        console.error(err);
        $("#btnAddToCartmanual").removeAttr('disabled').css('pointer-event', 'auto');
    });

}
async function SaveReciptientInfo() {
    let frm = "recptientInformation";
    if (await ValidateForm(frm) > 0) {
        return;
    }
    var enc = await InitilizeForm(frm);
   
    $("#btnSaveReciptientInfo").attr('disabled', 'disabled').css('pointer-event', 'none');
    await axios.post('/cart/SaveReciptientInfo', {}, {
        headers: {
            'ReciptientInfo': enc
        }
    }).then(function (res) {
        let data = res.data;
        console.info(res);
        $("#btnSaveReciptientInfo").removeAttr('disabled').css('pointer-event', 'auto');
        if (data.status) {
            LoadStep(3);        }
        else {

        }

    }).catch(function (err) {
        console.error(err);
        $("#btnSaveReciptientInfo").removeAttr('disabled').css('pointer-event', 'auto');
    });
}
async function ApplyCoppon() {
    var element = $("#CopponCode");
    if (element.val() == "" || element.val().length < 4) {
        element.focus().attr("placeholder", "Coppon code is required. ");
        return;
    }
    var enc = await InitilizeElement(element.val(), "CopponCode");
    $("#btnApplyCoppon").attr('disabled', 'disabled').css('pointer-event', 'none');
    await axios.post('/cart/UseCoppon', {}, {
        headers: {
            'CouponInfo': enc
        }
    }).then(function (res) {
        let data = res.data;
        console.info(res);
        $("#btnApplyCoppon").attr('disabled', 'disabled').css('pointer-event', 'none');
        if (data.status) {
            SuccessDialog(data.msg);
            LoadStep(1);
        }
        else {
            ErrorDialog(data.msg);
        }

    }).catch(function (err) {
        console.error(err);
        $("#btnApplyCoppon").attr('disabled', 'disabled').css('pointer-event', 'none');
    });
}

async function TestCheckout(type) {
    var st = $("#StepCounts").val();
    var reqPos = $("#requestPos").prop("checked");
    if (type == 1) {
        reqPos = false;
    }
    await axios.post('/cart/TestCheckout/' + type + "/" + reqPos, {}).then(function (res) {
        let data = res.data;
        console.info(res);
        //$("#btnAddToCart").removeAttr('disabled').css('pointer-event', 'auto');
        if (data.status) {
            LoadStep(4, data.data);
           }
        else {
            ErrorDialog(data.msg);
        }

    }).catch(function (err) {
        console.error(err);
        //$("#btnAddToCart").removeAttr('disabled').css('pointer-event', 'auto');
    });
}
async function GetBasketAmount() {
    await axios.post('/cart/GetTotalBasketAmount', {})
        .then(function (res) {
            var data = res.data;
            if (data.status) {
                $("#basketAmountHolder").html(data.data.count);
            }
        })
        .catch(function (err) { console.dir(err); });
}
GetBasketAmount();