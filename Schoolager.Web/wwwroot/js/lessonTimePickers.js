let startTimePicker = $("#startTime");
let endTimePicker = $("#endTime");

function setDropdownTimes(startDate, endDate = null) {
    console.log(startDate)
    console.log(endDate)
    startTimePicker.timepicker({
        'timeFormat': 'H:i',
        'minTime': '8:00',
        'maxTime': '17:15',
        'step': 15,
        'listWidth': 1,
    });

    endTimePicker.timepicker({
        'timeFormat': 'H:i',
        'minTime': '8:00',
        'maxTime': '17:15',
        'step': 45,
        'listWidth': 1,
    });

    startTimePicker.change(onStartTimeChange);
    endTimePicker.change(onEndTimeChange);

    let startTime = removeMinutes(new Date(startDate), 60);

    let endTime;

    // Shift the selected start time by 45 and 90 minutes
    let shiftedTimeHalfSlot = addMinutes(startTime, 45);
    let shiftedTimeFullSlot = addMinutes(startTime, 90);

    if (shiftedTimeFullSlot.getHours() >= 18) {
        shiftedTimeFullSlot = shiftedTimeHalfSlot;
    }

    if (endDate === null) {
        endTime = shiftedTimeHalfSlot;
    } else {
        endTime = removeMinutes(new Date(endDate), 60);
    }


    startTimePicker.timepicker('setTime', startTime);
    endTimePicker.timepicker('setTime', endTime);

    // Set the minimum allowed time for the end time
    endTimePicker.timepicker('option', { 'minTime': getShortTimeString(shiftedTimeHalfSlot) });
    endTimePicker.timepicker('option', { 'maxTime': getShortTimeString(shiftedTimeFullSlot) });

    // set the value of the hidden inputs, these values are the ones that will be used in the backEnd
    $("#startTimeHidden").attr('value', getShortTimeString(startTime));
    $("#endTimeHidden").attr('value', getShortTimeString(endTime));
}



// Set the allowed times for the end time drop down
// The end times are chosen based on the selected starttime
function onStartTimeChange() {
    // Get the selected value of the start time and end time
    let selectedTime = startTimePicker.timepicker('getTime');

    // Shift the selected start time by 30 minutes
    let shiftedTimeHalfSlot = addMinutes(selectedTime, 45);
    let shiftedTimeFullSlot = addMinutes(selectedTime, 90);

    if (shiftedTimeFullSlot.getHours() >= 18) {
        shiftedTimeFullSlot = shiftedTimeHalfSlot;
    }

    // Set the minimum allowed time for the end time
    endTimePicker.timepicker('option', { 'minTime': getShortTimeString(shiftedTimeHalfSlot) });
    endTimePicker.timepicker('option', { 'maxTime': getShortTimeString(shiftedTimeFullSlot) });

    // set the value of the hidden inputs, these values are the ones that will be used in the backEnd
    $("#startTimeHidden").attr('value', getShortTimeString(selectedTime));
    $("#endTimeHidden").attr('value', getShortTimeString(shiftedTimeHalfSlot));

    endTimePicker.timepicker('setTime', shiftedTimeHalfSlot);
}

function onEndTimeChange() {
    let selectedTime = endTimePicker.timepicker('getTime');

    $("#endTimeHidden").attr('value', getShortTimeString(selectedTime));
}

function getShortTimeString(date) {
    let hours = date.getHours();
    let minutes = date.getMinutes();

    if (minutes === 0) {
        return `${hours}:${minutes}0`;
    } else {
        return `${hours}:${minutes}`;
    }
}

// add a specific amount of minutes to a date
function addMinutes(date, minutes) {
    return new Date(date.getTime() + minutes * 60000);
}

function removeMinutes(date, minutes) {
    return new Date(date.getTime() - minutes * 60000);
}

function offsetDateTimezone(date) {
    return new Date(date.getTime() - (date.getTimezoneOffset() * 60000));
}