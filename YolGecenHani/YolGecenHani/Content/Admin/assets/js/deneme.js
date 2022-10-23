
function gp() {
    var charsL = "qwertyuopasdfghjklzxcvbnm";
    var charsU = "QWERTYUIOPASDFGHJKLZXCVBNM";
    var charsN = "0123456789";
    var charsC = "+-*/?!&%$";

    var passwordlength = 8;
    var sifre = '';
    if (passwordlength % 4 == 0) {
        for (var i = 0; i < passwordlength / 4; i++) {

            sifre += charsL[Math.floor(Math.random() * charsL.length)];
            sifre += charsU[Math.floor(Math.random() * charsU.length)];
            sifre += charsN[Math.floor(Math.random() * charsN.length)];
            sifre += charsC[Math.floor(Math.random() * charsC.length)];

        }
    }
    else {
        var kalan = passwordlength % 4;
        for (var i = 0; i < (passwordlength-kalan) / 4; i++) {

            sifre += charsL[Math.floor(Math.random() * charsL.length)];
            sifre += charsU[Math.floor(Math.random() * charsU.length)];
            sifre += charsN[Math.floor(Math.random() * charsN.length)];
            sifre += charsC[Math.floor(Math.random() * charsC.length)];

        }
        for (var i = 0; i < kalan; i++) {
            sifre += charsL[Math.floor(Math.random() * charsL.length)];
        }
    }
    
    
    
    document.getElementById("Password").value = sifre;
}