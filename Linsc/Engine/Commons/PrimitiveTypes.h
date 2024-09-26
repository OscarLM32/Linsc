
#pragma once

#include <stdint.h>

//unsigned ints
using ui64 = uint64_t;
using ui32 = uint32_t;
using ui16 = uint16_t;
using ui8  = uint8_t;

constexpr ui64 ui64_invalid_id{ 0xffff'ffff'ffff'ffffui64 };
constexpr ui32 ui32_invalid_id{ 0xffff'ffffui32 };
constexpr ui16 ui16_invalid_id{ 0xffffui16 };
constexpr ui8  ui8_invalid_id { 0xffui8 };

//signed ints
using i64 = int64_t;
using i32 = int32_t;
using i16 = int16_t;
using i8  = int8_t;

//floating points
using f32 = float;