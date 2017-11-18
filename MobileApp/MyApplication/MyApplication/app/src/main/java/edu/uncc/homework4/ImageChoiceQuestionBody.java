package edu.uncc.homework4;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.RadioButton;
import android.widget.RadioGroup;

import org.researchstack.backbone.model.Choice;
import org.researchstack.backbone.result.StepResult;
import org.researchstack.backbone.step.QuestionStep;
import org.researchstack.backbone.step.Step;
import org.researchstack.backbone.ui.step.body.SingleChoiceQuestionBody;

/**
 * Created by sunand on 11/8/17.
 */

public class ImageChoiceQuestionBody <T> extends SingleChoiceQuestionBody
{
    private Choice[] mChoices;

    public ImageChoiceQuestionBody(Step step, StepResult result) {
        super(step, result);

        QuestionStep questionStep = (QuestionStep)step;
        ImageChoiceAnswerFormat format = (ImageChoiceAnswerFormat)questionStep.getAnswerFormat();
        mChoices = format.getChoices();
    }

    @Override
    public View getBodyView(int viewType, LayoutInflater inflater, ViewGroup parent)
    {
        RadioGroup group = (RadioGroup)super.getBodyView(viewType, inflater, parent);

        for (int i=0; i<mChoices.length; i++) {

            RadioButton button = (RadioButton)group.getChildAt(i);
            button.setButtonDrawable(Integer.parseInt(mChoices[i].getValue().toString()));
        }

        return group;
    }
}
