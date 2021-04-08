# Student ID Card Read Sample

芝浦工業大学の学生証を読み取るアプリケーションのサンプル群です。
各言語での実装サンプルが収録されています。

## 動作環境
C++、C#はWindows 10にRC-S380を接続して動作確認済み。

PythonはArch LinuxにRC-S380を接続して動作確認済み。

## 環境構築
C++、C#（Windows）で利用する場合は、まず[NFCポートソフトウェア](https://www.sony.co.jp/Products/felica/consumer/download/felicaportsoftware.html)をインストールします。

Python（Linux）で利用する場合は、[nfcpy](https://nfcpy.readthedocs.io/en/latest/topics/get-started.html)をインストールします。
インストールは `$ pip install nfcpy==1.0.3` で可能です。
また、pythonディレクトリに移動して `$ pipenv install` をすることで仮想環境にインストールされます。
root権限なしで実行したい場合、 `$ python -m nfc` を実行して表示される指示に従ってください。

## 依存ライブラリ
C++、C#は[FeliCa Library](https://ja.osdn.net/projects/felicalib/)を利用しています。
ライブラリは同梱されています。

Pythonは[nfcpy](https://nfcpy.readthedocs.io/en/latest/topics/get-started.html)を利用しています。
インストールは環境構築に従って行ってください。
