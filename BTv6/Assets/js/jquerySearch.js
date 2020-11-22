//Jquery Search
function search() {
    $("#txtsearch").on("keyup", function () {
        var txtenter = $(this).val();
        $("table tr").each(function (results) {
            if (results !== 0) {
                var id = $(this).find("td:nth-child(1)").text();
                var id1 = $(this).find("td:nth-child(2)").text();
                var id2 = $(this).find("td:nth-child(3)").text();
                var id3 = $(this).find("td:nth-child(4)").text();
                var id4 = $(this).find("td:nth-child(5)").text();
                var id5 = $(this).find("td:nth-child(6)").text();
                var id6 = $(this).find("td:nth-child(7)").text();
                var id7 = $(this).find("td:nth-child(8)").text();
                var id8 = $(this).find("td:nth-child(9)").text();
                var ex = id.indexOf(txtenter) !== 0 && id.toLowerCase().indexOf(txtenter.toLowerCase());
                var ex1 = id1.indexOf(txtenter) !== 0 && id1.toLowerCase().indexOf(txtenter.toLowerCase());
                var ex2 = id2.indexOf(txtenter) !== 0 && id2.toLowerCase().indexOf(txtenter.toLowerCase());
                var ex3 = id3.indexOf(txtenter) !== 0 && id3.toLowerCase().indexOf(txtenter.toLowerCase());
                var ex4 = id4.indexOf(txtenter) !== 0 && id4.toLowerCase().indexOf(txtenter.toLowerCase());
                var ex5 = id5.indexOf(txtenter) !== 0 && id5.toLowerCase().indexOf(txtenter.toLowerCase());
                var ex6 = id6.indexOf(txtenter) !== 0 && id6.toLowerCase().indexOf(txtenter.toLowerCase());
                var ex7 = id7.indexOf(txtenter) !== 0 && id7.toLowerCase().indexOf(txtenter.toLowerCase());
                var ex8 = id8.indexOf(txtenter) !== 0 && id8.toLowerCase().indexOf(txtenter.toLowerCase());
                var condition = ex < 0 && ex1 < 0 && ex2 < 0 && ex3 < 0 && ex4 < 0 && ex5 < 0 && ex6 < 0 && ex7 < 0 && ex8 < 0
                if (condition) {
                    $(this).hide();
                }
                else {
                    $(this).show();
                }
            }
        })
    })
}
