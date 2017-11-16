package edu.uncc.homework4;

import android.content.Intent;

import java.io.Serializable;

/**
 * Created by sunand on 11/9/17.
 */

public class SurveyAnswer implements Serializable {
    int QuestionNumber,AnswerValue;
    String AnswerText;

    public int getQuestionNumber() {
        return QuestionNumber;
    }

    public void setQuestionNumber(int questionNumber) {
        QuestionNumber = questionNumber;
    }

    public int getAnswerValue() {
        return AnswerValue;
    }

    public void setAnswerValue(int answerValue) {
        AnswerValue = answerValue;
    }

    public String getAnswerText() {
        return AnswerText;
    }

    public void setAnswerText(String answerText) {
        AnswerText = answerText;
    }
}
