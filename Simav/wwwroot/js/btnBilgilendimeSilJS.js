function reply_click(clicked_id) {
    id = clicked_id;
    swal({
        title: "Onaylama",
        text: "Bilgilendirme Silinecektir",
        type: "warning",
        showCancelButton: true, //iptal butonunu göster
        confirmButtonText: "Evet", //Onay vereceğimiz butonun üzerinde ne görünsün =>(basılırsa true)
        cancelButtonText: "Hayır", //İptal butonu üzerinde ne grünsün(ne yazsın) =>(basılırsa false)
        showLoaderOnConfirm: true //Yükleniyor ifadesi görünsün mü?
    }, function (isConfirm) {
        if (isConfirm)//isConfirm==true ise yani doğrulanmış ise (evet denmişse)   
        {
            SilmeIslemi(id);
        }
        else {
            toastr.warning("Silme işlemi İptal Edildi!");
        }
    });
}

function SilmeIslemi(id) {
    setTimeout(function () {
        $.ajax({
            method: 'POST',
            //data: { pMasraf: parseFloat($('#txtMasraflar').val()), pGelir: parseFloat($('#txtGelirler').val()) },
            data: { pId: id },
            url: '/Bilgilendirme/BilgilendirmeSil',
            beforeSend: function () {

            }
        }).done(function (veri) {
            console.log(veri);
            if (veri.basarili) {
                SilmeBasarili(veri);
            }
            else {
                SilmeBasarisiz(veri.mesaj);
            }

        }).fail(function () {

        }).always(function () {

        });
    }, 1000);
}
function SilmeBasarili(veri) {
    //alert("İşlem Başarıyla Tamamlandı");
    setTimeout(function () {
        swal({
            title: "Uyarı",
            text: "Silme işlemi Başarıyla Gerçekleşti!",
            type: "warning",
            showCancelButton: false, //iptal butonunu göster
            confirmButtonText: "Onayla", //Onay vereceğimiz butonun üzerinde ne görünsün =>(basılırsa true)
            showLoaderOnConfirm: true //Yükleniyor ifadesi görünsün mü?
        }, function (isConfirm) {
            location.reload();

        });
    }, 1000);
}
function SilmeBasarisiz(mesaj) {
    swal({
        title: "Silme İşlemi",
        text: mesaj,
        type: "warning"
    });
    toastr.warning("İşlem Başarısız!");
}