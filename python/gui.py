#!/usr/bin/env python3
import tkinter
import threading
import os
from sit_idcardlib_py import Reader

FILE_NAME = "ids.txt"

class App(tkinter.Frame):
    def __init__(self, master=None):
        super().__init__(master)
        self.master = master
        self.pack()
        self.create_widgets()

    def create_widgets(self):
        self.id_label = tkinter.Label(
            self,
            text="Not detected",
            padx=5,
            pady=10,
            font=("", 36)
        )
        self.id_label.pack(side=tkinter.TOP)

root = tkinter.Tk()
app = App(master=root)

def callback(card):
    global app
    global reader
    try:
        app.id_label["text"] = card.id
        found = False
        if os.path.isfile(FILE_NAME):
            with open(FILE_NAME) as f:
                while True:
                    student_id = f.readline()
                    if not student_id:
                        break
                    student_id = student_id.strip()
                    if student_id == card.id:
                        found = True
                        break
        if not found:
            with open(FILE_NAME, "a") as f:
                f.write("{}\n".format(card.id))
    except Exception as e:
        print(e)
reader = Reader(callback)

def thread():
    global reader
    while True:
        try:
            reader.read()
        except Exception as e:
            print(e)
threading.Thread(target=thread).start()

app.mainloop()
