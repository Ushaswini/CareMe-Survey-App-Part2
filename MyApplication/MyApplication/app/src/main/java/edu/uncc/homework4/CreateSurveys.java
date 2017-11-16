package edu.uncc.homework4;

import android.content.Intent;

import org.researchstack.backbone.answerformat.AnswerFormat;
import org.researchstack.backbone.answerformat.ChoiceAnswerFormat;
import org.researchstack.backbone.answerformat.IntegerAnswerFormat;
import org.researchstack.backbone.answerformat.TextAnswerFormat;
import org.researchstack.backbone.model.Choice;
import org.researchstack.backbone.step.InstructionStep;
import org.researchstack.backbone.step.QuestionStep;
import org.researchstack.backbone.step.Step;
import org.researchstack.backbone.task.OrderedTask;
import org.researchstack.backbone.ui.ViewTaskActivity;

import java.util.ArrayList;
import java.util.List;

/**
 * Created by sunand on 11/9/17.
 */

public class CreateSurveys {

    public static OrderedTask createInfo1Survey(){

        List<Step> steps = new ArrayList<>();

        //Instructions for survey
        InstructionStep instructionStep = new InstructionStep("survey_instruction_step",
                "Welcome!",
                "We need to collect just a little health information from you before we begin. Circle the correct answer");
        steps.add(instructionStep);


        AnswerFormat questionFormat = new ChoiceAnswerFormat(AnswerFormat.ChoiceAnswerStyle
                .SingleChoice,
                new Choice<>("Yes", 0),
                new Choice<>("No", 1));

        QuestionStep questionStep = new QuestionStep("quest_info_1", "Has your doctor told you that you have high blood pressure?", questionFormat);
        questionStep.setPlaceholder("Quest");
        questionStep.setOptional(false);
        steps.add(questionStep);

        return new OrderedTask("survey_task", steps);
    }

    public static OrderedTask createInfo2Survey(){

        List<Step> steps = new ArrayList<>();
        AnswerFormat questionFormat = new ChoiceAnswerFormat(AnswerFormat.ChoiceAnswerStyle
                .SingleChoice,
                new Choice<>("Yes", 0),
                new Choice<>("No", 1));

        QuestionStep questionStep = new QuestionStep("quest_info_2", "Has your doctor prescribed medication or pills to treat your high blood pressure?", questionFormat);
        questionStep.setPlaceholder("Quest");
        questionStep.setOptional(false);
        steps.add(questionStep);

        return new OrderedTask("survey_task", steps);
    }

    public static OrderedTask createMainSurvey(boolean skipToDietSection){

        List<Step> steps = new ArrayList<>();
        AnswerFormat questionFormat;
        InstructionStep instructionStep;
        QuestionStep questionStep;

        instructionStep = new InstructionStep("survey_instruction_step",
                "Part 1",
                "The following questions ask about your health behavior activities during the past 7 days. For each question, circle the number of days that you performed that activity.");
        steps.add(instructionStep);

        questionFormat = new ChoiceAnswerFormat(AnswerFormat.ChoiceAnswerStyle
                .SingleChoice,
                new Choice<>("0", 0),
                new Choice<>("1", 1),
                new Choice<>("2", 2),
                new Choice<>("3", 3),
                new Choice<>("4", 4),
                new Choice<>("5", 5),
                new Choice<>("6", 6),
                new Choice<>("7", 7),
                new Choice<>("I have not been prescribed blood pressure pills. ", 8));

        if(!skipToDietSection) {

            questionStep = new QuestionStep("1", "Take your blood pressure pills?", questionFormat);
            questionStep.setPlaceholder("Quest");
            questionStep.setOptional(false);
            steps.add(questionStep);

            questionStep = new QuestionStep("2", "Take your blood pressure pillsat the same time everyday?", questionFormat);
            questionStep.setPlaceholder("Quest");
            questionStep.setOptional(false);
            steps.add(questionStep);

            questionStep = new QuestionStep("3", "Take the recommended number ofblood pressure pills?", questionFormat);
            questionStep.setPlaceholder("Quest");
            questionStep.setOptional(false);
            steps.add(questionStep);
        }

        questionFormat = new ChoiceAnswerFormat(AnswerFormat.ChoiceAnswerStyle
                .SingleChoice,
                new Choice<>("0", 0),
                new Choice<>("1", 1),
                new Choice<>("2", 2),
                new Choice<>("3", 3),
                new Choice<>("4", 4),
                new Choice<>("5", 5),
                new Choice<>("6", 6),
                new Choice<>("7", 7),
                new Choice<>("I am allergic to nuts. ", 8));

        questionStep = new QuestionStep("4", "Eat nuts or peanut butter?", questionFormat);
        questionStep.setPlaceholder("Quest");
        questionStep.setOptional(false);
        steps.add(questionStep);

        questionFormat = new ChoiceAnswerFormat(AnswerFormat.ChoiceAnswerStyle
                .SingleChoice,
                new Choice<>("0", 0),
                new Choice<>("1", 1),
                new Choice<>("2", 2),
                new Choice<>("3", 3),
                new Choice<>("4", 4),
                new Choice<>("5", 5),
                new Choice<>("6", 6),
                new Choice<>("7", 7));

        questionStep = new QuestionStep("5", "Eat beans, peas, or lentils?", questionFormat);
        questionStep.setPlaceholder("Quest");
        questionStep.setOptional(false);
        steps.add(questionStep);

        questionStep = new QuestionStep("6", "Eat eggs?", questionFormat);
        questionStep.setPlaceholder("Quest");
        questionStep.setOptional(false);
        steps.add(questionStep);

        questionStep = new QuestionStep("7", "Eat pickles, olives, or other vegetables in brine?", questionFormat);
        questionStep.setPlaceholder("Quest");
        questionStep.setOptional(false);
        steps.add(questionStep);

        questionStep = new QuestionStep("8", "Eat five or more servings of fruits and vegetables?", questionFormat);
        questionStep.setPlaceholder("Quest");
        questionStep.setOptional(false);
        steps.add(questionStep);

        questionStep = new QuestionStep("9", "Eat more than one serving of fruit (fresh, frozen, canned or fruit juice)?", questionFormat);
        questionStep.setPlaceholder("Quest");
        questionStep.setOptional(false);
        steps.add(questionStep);

        questionStep = new QuestionStep("10", "Eat more than one serving of vegetables?", questionFormat);
        questionStep.setPlaceholder("Quest");
        questionStep.setOptional(false);
        steps.add(questionStep);

        questionStep = new QuestionStep("11", "Drink milk (in a glass, with cereal, or in coffee, tea or cocoa)?", questionFormat);
        questionStep.setPlaceholder("Quest");
        questionStep.setOptional(false);
        steps.add(questionStep);

        questionStep = new QuestionStep("12", "Eat broccoli, collard greens, spinach, potatoes, squash or sweet potatoes?", questionFormat);
        questionStep.setPlaceholder("Quest");
        questionStep.setOptional(false);
        steps.add(questionStep);

        questionStep = new QuestionStep("13", "Eat apples, bananas, oranges, melon or raisins?", questionFormat);
        questionStep.setPlaceholder("Quest");
        questionStep.setOptional(false);
        steps.add(questionStep);

        questionStep = new QuestionStep("14", "Eat whole grain breads, cereals, grits, oatmeal or brown rice?", questionFormat);
        questionStep.setPlaceholder("Quest");
        questionStep.setOptional(false);
        steps.add(questionStep);

        questionStep = new QuestionStep("15", "Do at least 30 minutes total of physical activity?", questionFormat);
        questionStep.setPlaceholder("Quest");
        questionStep.setOptional(false);
        steps.add(questionStep);

        questionStep = new QuestionStep("16", "Do a specific exercise activity (such as swimming, walking, or biking) other than what you do around the house or as part of your work?", questionFormat);
        questionStep.setPlaceholder("Quest");
        questionStep.setOptional(false);
        steps.add(questionStep);


        questionStep = new QuestionStep("17", "Engage in weight lifting or strength training (other than what you do around the house or as part of your work)?", questionFormat);
        questionStep.setPlaceholder("Quest");
        questionStep.setOptional(false);
        steps.add(questionStep);


        questionStep = new QuestionStep("18", "Do any repeated heavy lifting or pushing/pulling of heavy items either for your job oraround the house or garden?", questionFormat);
        questionStep.setPlaceholder("Quest");
        questionStep.setOptional(false);
        steps.add(questionStep);


        questionStep = new QuestionStep("19", "Smoke a cigarette or cigar, even just one puff?", questionFormat);
        questionStep.setPlaceholder("Quest");
        questionStep.setOptional(false);
        steps.add(questionStep);

        questionStep = new QuestionStep("20", "Stay in a room or ride in an enclosed vehicle while someone was smoking?", questionFormat);
        questionStep.setPlaceholder("Quest");
        questionStep.setOptional(false);
        steps.add(questionStep);

        instructionStep = new InstructionStep("survey_instruction_step_2",
                "Part 2",
                "The following questions ask about your efforts to manage your weight during the last 30 days. If you were sick during the past month, please think back to the previous month that you were not sick. Circlethe one answerthat best describes what you do to lose weight or maintain your weight.");
        steps.add(instructionStep);

        questionFormat = new ChoiceAnswerFormat(AnswerFormat.ChoiceAnswerStyle
                .SingleChoice,
                new Choice<>("Strongly Disagree", 1),
                new Choice<>("Disagree", 2),
                new Choice<>("Not Sure", 3),
                new Choice<>("Agree", 4),
                new Choice<>("Strongly Agree", 5));

        questionStep = new QuestionStep("21", "I am careful about what I eat.", questionFormat);
        questionStep.setPlaceholder("Quest");
        questionStep.setOptional(false);
        steps.add(questionStep);

        questionStep = new QuestionStep("22", "I read food labels when I grocery shop.", questionFormat);
        questionStep.setPlaceholder("Quest");
        questionStep.setOptional(false);
        steps.add(questionStep);

        questionStep = new QuestionStep("23", "I exercise in order to lose or maintain weight.", questionFormat);
        questionStep.setPlaceholder("Quest");
        questionStep.setOptional(false);
        steps.add(questionStep);

        questionStep = new QuestionStep("24", "I have cut out drinking sugary sodas and sweet tea.", questionFormat);
        questionStep.setPlaceholder("Quest");
        questionStep.setOptional(false);
        steps.add(questionStep);

        questionStep = new QuestionStep("25", "I eat smaller portions or eat fewer portions.", questionFormat);
        questionStep.setPlaceholder("Quest");
        questionStep.setOptional(false);
        steps.add(questionStep);

        questionStep = new QuestionStep("26", "I have stopped buying or bringing unhealthy foods into my home.", questionFormat);
        questionStep.setPlaceholder("Quest");
        questionStep.setOptional(false);
        steps.add(questionStep);

        questionStep = new QuestionStep("27", "I have cut out or limit some foods that I like but that are not good for me.", questionFormat);
        questionStep.setPlaceholder("Quest");
        questionStep.setOptional(false);
        steps.add(questionStep);

        questionStep = new QuestionStep("28", "I eat at restaurants or fast food places less often.", questionFormat);
        questionStep.setPlaceholder("Quest");
        questionStep.setOptional(false);
        steps.add(questionStep);

        questionStep = new QuestionStep("29", "I substitute healthier foods for things that I used to eat.", questionFormat);
        questionStep.setPlaceholder("Quest");
        questionStep.setOptional(false);
        steps.add(questionStep);

        questionStep = new QuestionStep("30", "I have modified my recipes when I cook.", questionFormat);
        questionStep.setPlaceholder("Quest");
        questionStep.setOptional(false);
        steps.add(questionStep);


        instructionStep = new InstructionStep("survey_instruction_step_3",
                "Part 3",
                "The next three questions are about alcohol consumption. A drink of alcohol is defined as:" +
                        "One, 12 oz. can or bottle of beer;" +
                        "One, 4 ounce glass of wine;" +
                        "One, 12 oz. can or bottle of wine cooler;" +
                        "One mixed drink or cocktail;" +
                        "Or 1 shot of hard liquor.");
        steps.add(instructionStep);

        questionFormat = new ChoiceAnswerFormat(AnswerFormat.ChoiceAnswerStyle
                .SingleChoice,
                new Choice<>("0", 0),
                new Choice<>("1", 1),
                new Choice<>("2", 2),
                new Choice<>("3", 3),
                new Choice<>("4", 4),
                new Choice<>("5", 5),
                new Choice<>("6", 6),
                new Choice<>("7", 7));


        questionStep = new QuestionStep("31", "On average, how many days per week do you drink alcohol?", questionFormat);
        questionStep.setPlaceholder("Quest");
        questionStep.setOptional(false);
        steps.add(questionStep);

        IntegerAnswerFormat format = new IntegerAnswerFormat(0,20);


        questionStep = new QuestionStep("32", "On a typical day that you drink alcohol, how many drinks do you have?",  format);
        questionStep.setPlaceholder("Quest");
        questionStep.setOptional(false);
        steps.add(questionStep);

        questionStep = new QuestionStep("33", "What is the largest number of drinks that you’ve had on any given day within the last month?", format);
        questionStep.setPlaceholder("Quest");
        questionStep.setOptional(false);
        steps.add(questionStep);



        return new OrderedTask("survey_task", steps);

    }
}
