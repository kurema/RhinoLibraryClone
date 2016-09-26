# RhinoLibraryClone
Rhino Common SDKのうち必要な部分を適宜実装します。自分用のまとめです。コピペでお使いください。

## 意図的な変更部分
1. 元のSDKではRhinoMath.UnsetValueで未設定に相当する定数を割り当てているようですが、このライブラリでは同じアプローチは取りません。  
未設定を示したいならnull許容型か独自に定義したstructを使う方が望ましいと思われるからです。
2. floatで構成される型は今のところ実装するつもりはありません。(Point3fとか)
3. GeometryLibraryと同じく任意次元に対応したPointGeneral型を採用しています。
