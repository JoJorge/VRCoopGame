using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataGenerator {

    public static Data generateDataByContent<T> (string header, T content) {
        if (content is string) {
            return new StringData (header, (string)(object)content);
        }
        else if (content is Sprite) {
            return new ImageData (header, (Sprite)(object)content);
        }
        else {
            return new Data ();
        }
    }
}
