export class RegX {

    // 8 digit mobile number
    static MOBILENO_REGEX = /[0-9]{8}$/;
    // email regex
    static EMAIL_REGEX = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    // 'DD/MM/YYYY'
    static DOB_REGEX = /(^(((0[1-9]|[12][0-8])[\/](0[1-9]|1[012]))|((29|30|31)[\/](0[13578]|1[02]))|((29|30)[\/](0[4,6,9]|11)))[\/](19|[2-9][0-9])\d\d$)|(^29[\/]02[\/](19|[2-9][0-9])(00|04|08|12|16|20|24|28|32|36|40|44|48|52|56|60|64|68|72|76|80|84|88|92|96)$)/;
    // 'DD/MM/YYYY'
    static DATE_REGEX = /^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$/;
    // https://www.google.com
    // http://www.google.com
    // www.google.net
    // www.google.lk
    // google.com
    static URL = /^(http:\/\/www\.|https:\/\/www\.|http:\/\/|https:\/\/)?[a-z0-9]+([\-\.]{1}[a-z0-9]+)*\.[a-z]{2,5}(:[0-9]{1,5})?(\/.*)?$/;
    // Allow only English chars and special chars
    static ALLOWED_CHARS = /^[a-zA-Z0-9?><;,{}\-+=!@#$%^&*`'.~!”()\/\[\\\]\_{|}": ]+$/;
    // Maximum 15 numbers and maximum 2 desimal 
    static AMOUNT = /^\d{0,15}\.?\d{0,2}$/;
    // 0-9 Numbers only 
    static NUMERIC_NUMBERS = /^[0-9]+$/;
}