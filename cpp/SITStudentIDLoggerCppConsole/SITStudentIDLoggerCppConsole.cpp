#include <iostream>
#include <string>
#include <fstream>
#include "felicalib.h"

int main(int argc, char* argv[])
{
    std::cout << "SIT Student ID Logger C++ Console" << std::endl;
    std::cout << "Copyright (c) 2021 cordx56" << std::endl;

    std::string filename = "ids.txt";
    if (1 < argc) {
        filename = argv[1];
    }

    pasori* p;
    felica* f;
    p = pasori_open(NULL);
    if (!p) {
        std::cerr << "PaSoRi open failed!" << std::endl;
        exit(1);
    }
    pasori_init(p);

    std::string before_read_student_id = "";
    while (1) {
        f = felica_polling(p, 0x8277, 0, 0);
        if (!f) {
            //std::cerr << "Polling card failed" << std::endl;
            continue;
            //exit(1);
        }
        uint8 data[17];
        data[16] = 0;
        if (felica_read_without_encryption02(f, 0x010b, 0, 0, data) == 0) {
            std::string datastring = (char*)data;
            std::string student_id = datastring.substr(3, 7);

            if (before_read_student_id != student_id) {
                std::ofstream f;
                f.open(filename, std::ios_base::app);
                f << student_id << std::endl;
                std::cout << student_id << std::endl;
                before_read_student_id = student_id;
            }
        }
        felica_free(f);
    }
}

