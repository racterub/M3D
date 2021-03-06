﻿using M3D.Spooling.Common;
using M3D.Spooling.Preprocessors.Foundation;
using M3D.Spooling.Printer_Profiles;
using RepetierHost.model;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace M3D.Spooling.Core.Controllers.PrintJobs
{
  internal abstract class AbstractJob
  {
    private ThreadSafeVariable<JobStatus> status = new ThreadSafeVariable<JobStatus>();
    private JobDetails jobDetails;
    private Stopwatch job_begin_timer;

    public AbstractJob(JobParams jobParams, string user, InternalPrinterProfile printerProfile)
    {
      MyPrinterProfile = printerProfile;
      Status = JobStatus.Queued;
      PreviewImageFileName = "null";
      jobDetails = new JobDetails();
      Details.jobParams = jobParams;
      Details.bounds = new BoundingBox(float.MaxValue, float.MaxValue, float.MaxValue, float.MinValue, float.MinValue, float.MinValue);
      Details.ideal_temperature = 0;
      User = user;
      job_begin_timer = new Stopwatch();
      InitialSpoolUsed = new FilamentSpool
      {
        filament_temperature = jobParams.filament_temperature,
        filament_type = jobParams.filament_type
      };
    }

    public abstract JobCreateResult Create(PrinterInfo printerInfo);

    public abstract void Update(PrinterInfo printerInfo);

    public abstract bool Start(out List<string> start_gcode);

    public abstract bool Pause(out List<string> pause_gcode, FilamentSpool spool);

    public abstract JobController.Result Resume(out List<string> resume_gcode, FilamentSpool spool);

    public abstract void Stop();

    public abstract GCode GetNextCommand();

    public abstract bool Aborted { get; }

    public abstract bool Done { get; }

    public abstract bool RetractionRequired { get; }

    public abstract float PercentComplete { get; }

    public abstract bool CanPauseImmediately { get; }

    public abstract ulong CurrentFileLineNumber { get; }

    public JobInfo GetInfo()
    {
      return new JobInfo(Details.jobParams.jobname, User, Status, PreviewImageFileName, PercentComplete, TimeRemaining, Details.jobParams);
    }

    public string JobName
    {
      get
      {
        return Details.jobParams.jobname;
      }
    }

    public string User { get; private set; }

    public string PreviewImageFileName { get; protected set; }

    public JobStatus Status
    {
      get
      {
        return status.Value;
      }
      protected set
      {
        status.Value = value;
      }
    }

    public JobDetails Details
    {
      get
      {
        return jobDetails;
      }
    }

    public bool Stopped
    {
      get
      {
        if (!Aborted)
        {
          return Done;
        }

        return true;
      }
    }

    public float EstimatedPrintTime
    {
      get
      {
        return Details.jobParams.estimatedTime;
      }
    }

    public bool AutoStarting
    {
      get
      {
        return Details.jobParams.options.autostart_ignorewarnings;
      }
    }

    public float TimeRemaining
    {
      get
      {
        var num = (float)(JobBeginTimer.ElapsedMilliseconds / 1000.0 / 60.0);
        if (num > 5.0)
        {
          if (Details.jobParams.estimatedTime > 0.0)
          {
            return Details.jobParams.estimatedTime - Details.jobParams.estimatedTime * PercentComplete;
          }

          if (PercentComplete > 0.0)
          {
            return (float) TimeSpan.FromMinutes(num / (double)PercentComplete - num).TotalSeconds;
          }
        }
        return 0.0f;
      }
    }

    public double MinutesElapsed
    {
      get
      {
        return JobBeginTimer.Elapsed.TotalMinutes;
      }
    }

    public InternalPrinterProfile MyPrinterProfile { get; private set; }

    protected void OnGetNextCommand()
    {
      if (JobBeginTimer.IsRunning)
      {
        return;
      }

      JobBeginTimer.Start();
    }

    protected Stopwatch JobBeginTimer
    {
      get
      {
        return job_begin_timer;
      }
    }

    protected FilamentSpool InitialSpoolUsed { get; private set; }

    public enum JobMode
    {
      DirectPrinting,
      PrintingToSDCard,
      PrintingToSDCardAutoStartPrint,
      FirmwarePrintingFromSDCard,
      SimultaneousSDSaveAndPrint,
    }
  }
}
