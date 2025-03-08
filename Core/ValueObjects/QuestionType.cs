using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ValueObjects
{
    public enum QuestionType
    {
        Grammar,             // Câu hỏi ngữ pháp
        Vocabulary,          // Câu hỏi từ vựng
        SentenceOrdering,    // Sắp xếp từ/câu
        ReadingComprehension,// Đọc hiểu và trả lời câu hỏi
        ClozeTest,           // Điền từ vào đoạn văn
        ErrorIdentification, // Tìm lỗi sai
        Pronunciation,       // Câu hỏi phát âm
        MultipleChoice,      // Chọn đáp án đúng
        FillInTheBlank,      // Điền vào chỗ trống
        FunctionalLanguage   // Câu hỏi giao tiếp/ngữ cảnh
    }

}
