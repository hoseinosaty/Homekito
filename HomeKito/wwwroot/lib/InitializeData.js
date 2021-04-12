///Hosein Osati Functions
async function IntilizeData(data) {
    var key = CryptoJS.enc.Utf8.parse('8080808080808080');
    var iv = CryptoJS.enc.Utf8.parse('8080808080808080');

    var initialized = CryptoJS.AES.encrypt(CryptoJS.enc.Utf8.parse(data), key,
        {
            keySize: 128 / 8,
            iv: iv,
            mode: CryptoJS.mode.CBC,
            padding: CryptoJS.pad.Pkcs7
        });



    return initialized;
}

async function InitilizeForm(frm, setJson = false) {
    var data = {};
    $(`#${frm} input,#${frm} select,#${frm} textarea`).each(function (i, el) {
        data[$(el).attr("name")] = $(el).val();
        console.warn(el);
    });
    return await IntilizeData(JSON.stringify(data));
}
async function InitilizeElement(val,name) {
    var data = {};
    data[name] = val;
    return await IntilizeData(JSON.stringify(data));
}