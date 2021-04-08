# SITStudentIDLoggerCppConsole

芝浦工業大学の学生証を読み取り学籍番号を記録するWindows向けC++アプリケーションです。

## 動作環境

Windows 10にRC-S380を接続して動作確認済み。

## 環境構築

まず[NFCポートソフトウェア](https://www.sony.co.jp/Products/felica/consumer/download/felicaportsoftware.html)をインストールします。

Visual Studio 2019をインストールしてプロジェクトを開きます。

## 依存ライブラリ

本アプリケーションは[FeliCa Library](https://ja.osdn.net/projects/felicalib/)を利用しています。ライブラリのソースコードは同梱されています。