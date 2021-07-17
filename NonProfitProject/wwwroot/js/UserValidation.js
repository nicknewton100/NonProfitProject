﻿//checks to make sure the decimal doesn't go over 2 decimal places 
//credit : https://stackoverflow.com/questions/46321683/javascript-restrict-input-once-2-decimal-places-have-been-reached
var CheckDecimal = function (e) {
    var t = e.value;
    e.value = (t.indexOf(".") >= 0) ? (t.substr(0, t.indexOf(".")) + t.substr(t.indexOf("."), 3)) : t;
}
//creates format credit card/debit card expiration date 
//credit: https://stackoverflow.com/questions/45259196/javascript-regex-credit-card-expiry-date-auto-format
function card_expires_format(e) {
    var string = e.value;
    return e.value = string.replace(
        /[^0-9]/g, '' // To allow only numbers
    ).replace(
        /^([2-9])$/g, '0$1' // To handle 3 > 03
    ).replace(
        /^(1{1})([3-9]{1})$/g, '0$1/$2' // 13 > 01/3
    ).replace(
        /^0{1,}/g, '0' // To handle 00 > 0
    ).replace(
        /^([0-1]{1}[0-9]{1})([0-9]{1,2}).*/g, '$1/$2' // To handle 113 > 11/3
    );
}

//creates format for credit cards
//credit: https://stackoverflow.com/questions/45877752/credit-card-input-format-javascript
function cc_format(value) {
    var value = value.replace(/[a-zA-Z]/, '')
    var value = value.replace(/[-!$ %^&* ()_ +| ~=`{}\[\]:";'<>?,.\/]/, "")
    var v = value.replace(/\s+/g, '').replace(/[^0-9]/gi, '')
    var matches = v.match(/\d{4,16}/g);
    var match = matches && matches[0] || ''
    var parts = []
    for (i = 0, len = match.length; i < len; i += 4) {
        parts.push(match.substring(i, i + 4))
    }
    if (parts.length) {
        return parts.join(' ')
    } else {
        return value
    }
}
//create format for cvv number
function cvv_format(e) {
    var cvv = e.value;
    if (/^(?!\d{4}).{4}$|^.{1,3}$|^.{5,}$/.test(cvv) == false) {
       cvv = cvv.substring(0,cvv.length - 1)
    }
    return e.value = cvv.replace(/^\d + (\.\d{ 1, 3}) ? $/, '').replace(/[a-zA-Z]/, '').replace(/[-!$ %^&* ()_ +| ~=`{}\[\]:";'<>?,.\/]/, '')
    
}
//creates format for postal code
function postalcode_format(e){
    var postalcode = e.value;
    if (/^(?!\d{6}).{6}$|^.{1,5}$|^.{7,}$/.test(postalcode) == false) {
        postalcode = postalcode.substring(0, postalcode.length - 1)
    }
    return e.value = postalcode.replace(/^\d + (\.\d{ 1, 3}) ? $/, '').replace(/[a-zA-Z]/, '').replace(/[-!$ %^&* ()_ +| ~=`{}\[\]:";'<>?,.\/]/, '')
}