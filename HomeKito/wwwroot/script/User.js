async function UpdateProfile() {
    let frm = "updateProfileFrm";
    var enc = await InitilizeForm(frm);
    $("#updateProfileBtn").attr("disabled", "disabled").css('pointer-event', 'none');
    await axios.post('/User/Update', {}, {
        headers: {
            'UpdateData': enc
        }
    }).then(function (res) {
        let data = res.data;
        console.info(res);
        $("#updateProfileBtn").removeAttr("disabled").css('pointer-event', 'auto');
        if (data.status) {
            window.location.href = window.location.href;
        }
        else {
            if ('EXPIREDTOKEN' in data.data) {
                location.href = "/User";
            }
            else {
                alert(data.msg);
            }
        }

    }).catch(function (err) {
        $("#updateProfileBtn").removeAttr("disabled").css('pointer-event', 'auto');
        console.error(err);

    });
}

function removeTr(t, type, id) {
    var elTr = $(t).parent().parent();
    var model = {
        A_Id: id
    }
    axios.post("/api/cpanel/User/DeleteAddress", model).then(function(res){

    }).catch(function (err) {

    })
    elTr.remove()
}
function getAddress(user) {
    var address = $(".getAddress").val();
    var oddEven = $("table#addresslist tbody tr:last-child").attr("class"); var eo;
    if (address.length > 3) {
        var lastChildAddres = parseInt($("table#addresslist tbody tr:last-child td:first-child").text());
        (oddEven == "even") ? eo = "odd" : eo = "even";
        var elRadio = `
                <tr role="row" class="${eo}">
                    <td class="sorting_1">
                        ${lastChildAddres + 1}
                    </td>
                    <td>
                        ${address}
                    </td>
                    <td><span class="success">فعال</span></td>
                    <td><button class="btnremove fas fa-times" onclick="removeTr(this,'address',15)"></button></td>
                </tr>
        `;

        $("table#addresslist tbody").append(elRadio);
        var model = {
            A_UserId: user,
            A_Address: $("#inputaddres").val()
        }
        axios.post("/api/cpanel/User/AddAddress", model).then(function (res) {
            hrb_notify([
                'success',
                '',
                'fa-map-marked-alt',
                "آدرس جدید با موفقیت ثبت شد",
                'bottomLeft',
                'flipInY',
                'flipOutX',
                '5'
            ]);
            $(".getAddress").val('');
        }).catch(function (err) {
            hrb_notify([
                'danger',
                '',
                'fa-map-marked-alt',
                "لطفا آدرس را به درستی وارد نمایید",
                'bottomLeft',
                'flipInY',
                'flipOutX',
                '5'
            ]);
        })

    }
}