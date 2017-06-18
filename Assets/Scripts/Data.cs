using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data {

    private string header;
    private object content;
    protected System.Type type;

    public Data(){
    }
    protected Data(string header, object content) {
        this.header = header;
        this.content = content;
    }
    public string getHeader() {
        return header;
    }
    public object getContent() {
        return content;
    }
    public System.Type getType() {
        return type;
    }
}
