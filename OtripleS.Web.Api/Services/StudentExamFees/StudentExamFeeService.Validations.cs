﻿//---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
//----------------------------------------------------------------

using System;
using OtripleS.Web.Api.Models.StudentExamFees;
using OtripleS.Web.Api.Models.StudentExamFees.Exceptions;

namespace OtripleS.Web.Api.Services.StudentExamFees
{
    public partial class StudentExamFeeService
    {
        private void ValidateStudentExamFeeOnCreate(StudentExamFee studentExamFee)
        {
            ValidateStudentExamFeeIsNull(studentExamFee);
            ValidateStudentExamFeeIdsAreNull(studentExamFee.Id, studentExamFee.StudentId, studentExamFee.ExamFeeId);
            ValidateInvalidAuditFields(studentExamFee);
        }

        private void ValidateStudentExamFeeIsNull(StudentExamFee studentExamFee)
        {
            if (studentExamFee is null)
            {
                throw new NullStudentExamFeeException();
            }
        }

        private void ValidateStudentExamFeeIdsAreNull(Guid studentExamFeeId, Guid studentId, Guid examFeeId)
        {
            if (studentExamFeeId == default)
            {
                throw new InvalidStudentExamFeeException(
                    parameterName: nameof(StudentExamFee.Id),
                    parameterValue: studentExamFeeId);
            }

            if (studentId == default)
            {
                throw new InvalidStudentExamFeeException(
                    parameterName: nameof(StudentExamFee.StudentId),
                    parameterValue: studentId);
            }

            if (examFeeId == default)
            {
                throw new InvalidStudentExamFeeException(
                    parameterName: nameof(StudentExamFee.ExamFeeId),
                    parameterValue: examFeeId);
            }
        }

        private void ValidateInvalidAuditFields(StudentExamFee studentExamFee)
        {
            switch (studentExamFee)
            {
                case { } when IsInvalid(studentExamFee.CreatedBy):
                    throw new InvalidStudentExamFeeException(
                    parameterName: nameof(StudentExamFee.CreatedBy),
                    parameterValue: studentExamFee.CreatedBy);

                case { } when IsInvalid(studentExamFee.UpdatedBy):
                    throw new InvalidStudentExamFeeException(
                    parameterName: nameof(StudentExamFee.UpdatedBy),
                    parameterValue: studentExamFee.UpdatedBy);

                case { } when IsInvalid(studentExamFee.CreatedDate):
                    throw new InvalidStudentExamFeeException(
                    parameterName: nameof(StudentExamFee.CreatedDate),
                    parameterValue: studentExamFee.CreatedDate);

                case { } when IsInvalid(studentExamFee.UpdatedDate):
                    throw new InvalidStudentExamFeeException(
                    parameterName: nameof(StudentExamFee.UpdatedDate),
                    parameterValue: studentExamFee.UpdatedDate);
            }
        }

        private static bool IsInvalid(Guid input) => input == default;
        private static bool IsInvalid(DateTimeOffset input) => input == default;
    }
}
