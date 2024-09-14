#include <iostream>
#include <cstdint>
#include <conio.h>

int main() {
    setlocale(LC_ALL, "");
    int8_t A[8] = { 1, 2, 3, 4, 5, 6, 7, 8 };
    int8_t B[8] = { 9, 10, 11, 12, 13, 14, 15, 16 };
    int8_t C[8] = { 17, 18, 19, 20, 21, 22, 23, 24 };
    int16_t D[8] = { 25, 26, 27, 28, 29, 30, 31, 32 };
    int16_t F[8] = { 0 };

    __asm__ __volatile__ (
        "movq (%1), %%mm0nt"
        "movq (%2), %%mm1nt"
        "movq (%3), %%mm2nt"
        "movq (%4), %%mm3nt"
        "punpcklbw %%mm0, %%mm4nt"
        "punpckhbw %%mm0, %%mm5nt"
        "punpcklbw %%mm1, %%mm6nt"
        "punpckhbw %%mm1, %%mm7nt"
        "pxor %%mm0, %%mm0nt"
        "pxor %%mm1, %%mm1nt"
        "punpcklbw %%mm2, %%mm0nt"
        "punpckhbw %%mm2, %%mm1nt"
        "pxor %%mm2, %%mm2nt"
        "movq 8+%4, %%mm2nt"
        "psrlw $8, %%mm0nt"
        "psrlw $8, %%mm1nt"
        "paddsw %%mm6, %%mm4nt"
        "pxor %%mm6, %%mm6nt"
        "movq %%mm4, %%mm6nt"
        "pmullw %%mm0, %%mm4nt"
        "pmulhw %%mm0, %%mm6nt"
        "psllw $4, %%mm6nt"
        "psrlw $4, %%mm4nt"
        "paddsw %%mm4, %%mm6nt"
        "psrlw $4, %%mm6nt"
        "paddsw %%mm3, %%mm6nt"
        "movq %%mm6, (%0)nt"
        "paddsw %%mm7, %%mm5nt"
        "pxor %%mm7, %%mm7nt"
        "movq %%mm5, %%mm7nt"
        "pmullw %%mm1, %%mm7nt"
        "pmulhw %%mm1, %%mm5nt"
        "psllw $4, %%mm5nt"
        "psrlw $4, %%mm7nt"
        "paddsw %%mm5, %%mm7nt"
        "psrlw $4, %%mm7nt"
        "paddsw %%mm2, %%mm7nt"
        "movq %%mm7, 8+%0nt"
        : 
        : "r" (F), "r" (A), "r" (B), "r" (C), "r" (D)
        : "mm0", "mm1", "mm2", "mm3", "mm4", "mm5", "mm6", "mm7", "memory"
    );

    std::cout << "F[i]: ";
    for (auto i = 0; i < 8; ++i) {
        std::cout << F[i] << " ";
    }
    std::cout << std::endl;

    _getch();
    return 0;
}
