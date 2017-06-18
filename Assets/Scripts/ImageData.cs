using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageData : Data {

    public ImageData(string header, object content) : base(header, content) {
        this.type = typeof(Sprite);
    }}
