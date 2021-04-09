#include "sit_student_card.hpp"

sit_student_card::Reader::Reader() {
	this->p = pasori_open(NULL);
	if (!this->p) {
		throw std::runtime_error("PaSoRi open failed");
	}
	pasori_init(this->p);
}

sit_student_card::Reader::~Reader() {
	if (this->f) {
		felica_free(this->f);
	}
	if (this->p) {
		pasori_close(this->p);
	}
}

void sit_student_card::Reader::polling() {
	this->f = felica_polling(this->p, 0x8277, 0, 0);
	if (!this->f) {
		throw sit_student_card::PollingFailedException("ポーリング失敗");
	}
}

sit_student_card::Card sit_student_card::Reader::read() {
	sit_student_card::Card card;
	this->polling();
	uint8 data[16];
	if (felica_read_without_encryption02(this->f, 0x010b, 0, 0, data) != 0) {
		throw sit_student_card::ReadFailedException("学籍番号が読み取れません");
	}
	for (int i = 3; i < 10; i++) {
		card.id += (char)data[i];
	}
	if (felica_read_without_encryption02(this->f, 0x010b, 0, 1, data) != 0) {
		throw sit_student_card::ReadFailedException("有効期限が読み取れません");
	}
	for (int i = 0; i < 8; i++) {
		card.valid_from_raw += (char)data[i];
	}
	for (int i = 8; i < 16; i++) {
		card.valid_to_raw += (char)data[i];
	}
	felica_free(this->f);
	return card;
}


sit_student_card::PollingFailedException::PollingFailedException(const char* msg) : runtime_error(msg) {
}
sit_student_card::ReadFailedException::ReadFailedException(const char* msg) : runtime_error(msg) {
}