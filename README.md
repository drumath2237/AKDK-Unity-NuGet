# AKDK-Unity-NuGet

## About

[UnityNuGet](https://github.com/xoofx/UnityNuGet)
というパッケージレジストリを見つけたので、
そこから`Microsoft.Azure.Kinect.Sensor`をインポートして
実行してみるテスト。

## Env

- Windows 10 Home
- Unity 2020.3.20
- Azure Kinect

## Install & Usage

Azure Kinectを接続し、
`Assets/Scenes/SampleScene.unity`を実行します。

## Appendix

現状UnityNuGetではnetstandard2.0のパッケージのみに対応しているみたいで、
AzureKinect Sensor SDKのようにネイティブプラグインが
必要なパッケージは別途インストールする必要があるらしい。

今回も`k4a.dll`と`depthengine_2_0.dll`
は別途Pluginsにインポートしている。

## Contact

何かございましたら[にー兄さんのTwitter](https://twitter.com/ninisan_drumath)
までご連絡ください。
