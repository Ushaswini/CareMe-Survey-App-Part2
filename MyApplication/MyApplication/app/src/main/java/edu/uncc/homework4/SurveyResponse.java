package edu.uncc.homework4;

import java.io.Serializable;
import java.util.ArrayList;
import java.util.Date;

/**
 * Created by sunand on 11/9/17.
 */

public class SurveyResponse implements Serializable {
    String SurveyId, UserId,StudyGroupId;
    Date SurveyResponseReceivedTime;
    ArrayList<SurveyAnswer> data;

    public SurveyResponse(){
        data = new ArrayList<>();
    }


    public String getSurveyId() {
        return SurveyId;
    }

    public void setSurveyId(String surveyId) {
        SurveyId = surveyId;
    }

    public String getUserId() {
        return UserId;
    }

    public void setUserId(String userId) {
        UserId = userId;
    }

    public String getStudyGroupId() {
        return StudyGroupId;
    }

    public void setStudyGroupId(String studyGroupId) {
        StudyGroupId = studyGroupId;
    }

    public String getSurveyResponseReceivedTime() {
        return SurveyResponseReceivedTime.toString();
    }

    public void setSurveyResponseReceivedTime(Date surveyResponseReceivedTime) {
        SurveyResponseReceivedTime = surveyResponseReceivedTime;
    }

    public ArrayList<SurveyAnswer> getUserResponseText() {
        return data;
    }

    public void setUserResponseText(ArrayList<SurveyAnswer> userResponseText) {
        data = userResponseText;
    }
}
