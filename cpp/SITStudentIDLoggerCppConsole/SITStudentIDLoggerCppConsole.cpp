#include <iostream>
#include <string>
#include <fstream>
#include "sit_student_card.hpp"

int main(int argc, char* argv[])
{
    std::cout << "SIT Student ID Logger C++ Console" << std::endl;
    std::cout << "Copyright (c) 2021 cordx56" << std::endl;

    std::string filename = "ids.txt";
    if (1 < argc) {
        filename = argv[1];
    }

    try {
        sit_student_card::Reader r;

        std::string before_read_student_id = "";
        while (1) {
            try {
                sit_student_card::Card card = r.read();
                if (before_read_student_id != card.id) {
                    std::ofstream f;
                    f.open(filename, std::ios_base::app);
                    f << card.id << std::endl;
                    std::cout << card.id << std::endl;
                    before_read_student_id = card.id;
                }
            } catch (sit_student_card::PollingFailedException e) {
                //std::cerr << e.what() << std::endl;
            } catch (sit_student_card::ReadFailedException e) {
                //std::cerr << e.what() << std::endl;
            }
        }
    } catch (std::runtime_error e) {
        std::cerr << e.what() << std::endl;
    }
}