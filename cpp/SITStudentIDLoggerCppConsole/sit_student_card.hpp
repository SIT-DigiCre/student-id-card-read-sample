#ifndef SIT_STUDENT_CARD_HPP
#define SIT_STUDENT_CARD_HPP

#include <string>
#include <stdexcept>
#include "felicalib.h"

namespace sit_student_card {
	struct Card {
		std::string id;
		std::string valid_from_raw;
		std::string valid_to_raw;
	};

	class Reader {
	public:
		Reader();
		~Reader();
		void polling();
		Card read();
	private:
		pasori* p;
		felica* f;
	};

	class PollingFailedException : public std::runtime_error {
	public:
		PollingFailedException(const char*);
	};
	class ReadFailedException : public std::runtime_error {
	public:
		ReadFailedException(const char*);
	};
}

#endif