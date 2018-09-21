
function StartPing(pingFunction, delay)
{ var o = new CPing(pingFunction, delay, true); }
function CPing(pingFunction, delay, start) {
    if (start == null) start = true; oPing = this; this.pingFunction = pingFunction; this.iLongDelay = Math.max(delay, 2000); this.iShortDelay = 1000; this.handlePingResult = null; this.handlePingFailure = null; this.Start = function ()
    { this.Restart(false); }
    this.Restart = function (error)
    { var timeout = this.iLongDelay; if (error) timeout = this.iShortDelay; window.setTimeout("oPing.Ping();", timeout); }
    this.Ping = function ()
    { this.pingFunction(CPing_Success, CPing_Failure, this); }
    this.PingSuccess = function (result, context, method) {
        try {
            if (this.handlePingResult != null)
                this.handlePingResult(result, context, method);
        } catch (ex) { }
        this.Restart(false); var currentTime = new Date()
        var hours = currentTime.getHours()
        var minutes = currentTime.getMinutes()
        var sec = currentTime.getSeconds()
        if (minutes < 10)
            minutes = "0" + minutes
        if (sec < 10)
            sec = "0" + sec
        window.status = 'Ping Complete at: ' + hours + ':' + minutes + ':' + sec;
    }
    this.PingFailure = function (result, context, method) {
        try {
            if (this.handlePingFailure != null)
                this.handlePingFailure(result, context, method);
        }
        catch (ex) { }
        this.Restart(true);
    }
    window.CPing_Success = function (result, context, method) { oPing.PingSuccess(result, context, method); }
    window.CPing_Failure = function (result, context, method) { oPing.PingFailure(result, context, method); }
    if (start)
        this.Start();
}