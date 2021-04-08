#!/usr/bin/env python3
from sit_idcardlib_py import Reader

FILE_NAME = "ids.txt"

print("SIT Student ID Logger Python Console")
print("Copyright (c) 2021 cordx56")

before_read_student_id = ""

def callback(card):
    global before_read_student_id
    if card.id != before_read_student_id:
        print(card.id)
        with open(FILE_NAME, "a") as f:
            f.write("{}\n".format(card.id))
        before_read_student_id = card.id

reader = Reader(callback)

while True:
    try:
        reader.read()
    except Exception as e:
        print(e)
