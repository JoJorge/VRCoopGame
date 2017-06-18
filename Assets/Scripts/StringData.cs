using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StringData : Data {

    public StringData(string header, object content) : base(header, content) {
        this.type = typeof(string);
    }
}
