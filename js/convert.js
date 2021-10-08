

/**
 * image 转 base64 
 * @param img 要转换的img 
 * */ 
 this.imageToBase64 = function(img) {
    var canvas = document.createElement("canvas");
    canvas.width = img.width;
    canvas.height = img.height;
    var ctx = canvas.getContext("2d");
    ctx.drawImage(img, 0, 0, img.width, img.height);
    var ext = img.src.substring(img.src.lastIndexOf(".")+1).toLowerCase();
    var dataURL = canvas.toDataURL("image/"+ext);
    return dataURL;
}


/**
 * 从当前页面的url地址中获取参数数据
 * 支持url格式：?id=123&name=banana
 * @param pname 要获取的参数名，如果为空则返回以对像形式返回 
 * */
 this.getUrlQuery = function(pname) {
    let url = window.location.search; //获取url中"?"符后的字串
     
    let theRequest = new Object();
    if(url.indexOf("?") != -1) {
        let strs = url.substr(1);
        strs = strs.split("&");
        for(let i = 0; i < strs.length; i++) {
            theRequest[strs[i].split("=")[0]] = unescape(strs[i].split("=")[1]);
        }
    }
    if(pname){
        return theRequest[pname];
    }
    else{
        return theRequest;
    }
}

/*
 * 将Json对像转为URL参数串
 * 如： {act:'open',id:1} 
 * 结果为：act=open&id=1
 */
function ObjToUrlParams(obj, key) {
    var paramStr = "";
    if (obj instanceof String || obj instanceof Number || obj instanceof Boolean) {
        paramStr += "&" + key + "=" + encodeURIComponent(obj);
    } else {
        $.each(obj, function (i) {
            var k = !key ? i : key + (obj instanceof Array ? "[" + i + "]" : "." + i);
            paramStr += '&' + ObjToUrlParams(this, k);
        });
    }
    return paramStr.substr(1);
}

/**
 * 对输入的非数字值清理
 * @param {输入对像} obj 
 * @param {是否非数值提示} prompt 
 */
function clearNotNumForInput(obj, prompt) {
    var re = /[^\0-9\.]/g;
    if (prompt) {
        if (re.test(obj.value)) {
            alert('请仅输入数值型内容！');
        }
    }
    obj.value = obj.value.replace(re, '');
}
