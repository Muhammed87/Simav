$(document).ready(function () {
    $('#btnDelete').click(function () {
    //swal({}, function () { });

    swal({
        title: "Onaylama",
        text: "Haber Silinecektir",
        type: "warning",
        showCancelButton: true, //iptal butonunu göster
        confirmButtonText: "Evet", //Onay vereceğimiz butonun üzerinde ne görünsün =>(basılırsa true)
        cancelButtonText: "Hayır", //İptal butonu üzerinde ne grünsün(ne yazsın) =>(basılırsa false)
        showLoaderOnConfirm: true //Yükleniyor ifadesi görünsün mü?
    }, function (isConfirm) {
        if (isConfirm)//isConfirm==true ise yani doğrulanmış ise (evet denmişse)   
        {
            var id = parseFloat($('#btnDelete').val());
            console.log(id);
            SilmeIslemi(id);
        }
        else {
            toastr.warning("Silme işlemi İptal Edildi!");
        }
    });
});
function SilmeIslemi(id) {
    $.ajax({
        method: 'POST',
        //data: { pMasraf: parseFloat($('#txtMasraflar').val()), pGelir: parseFloat($('#txtGelirler').val()) },
        data: { pId: id },
        url: '/Haberler/HaberSil',
        beforeSend: function () {

        }
    }).done(function (veri) {
        console.log(veri);
        if (veri.basarili) {
            HesaplamaBasarili(veri);
        }
        else {
            HesaplamaBasarisiz(veri.mesaj);
        }

    }).fail(function () {

    }).always(function () {

    });
}
function HesaplamaBasarili(veri) {
    //alert("İşlem Başarıyla Tamamlandı");
    swal({
        title: "Hesaplama",
        text: veri.mesaj,
        type: "success"
    });
    toastr.success("İşlem Başarıyla Tamamlandı");
}
function HesaplamaBasarisiz(mesaj) {
    swal({
        title: "Hesaplama",
        text: mesaj,
        type: "warning"
    });
    toastr.warning("İşlem Başarısız!");
}
});