using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace FSC.Api.Mantenimiento.Modelos;

public partial class MantenimientoContext : DbContext
{
    public MantenimientoContext()
    {
    }

    public MantenimientoContext(DbContextOptions<MantenimientoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<FscCalendario> FscCalendarios { get; set; }

    public virtual DbSet<MachineDependence> MachineDependences { get; set; }

    public virtual DbSet<MsMachine> MsMachines { get; set; }

    public virtual DbSet<MsMachineCategory> MsMachineCategories { get; set; }

    public virtual DbSet<MsMachineCriticidad> MsMachineCriticidads { get; set; }

    public virtual DbSet<MsMachinePart> MsMachineParts { get; set; }

    public virtual DbSet<MsMachinePartsShDocument> MsMachinePartsShDocuments { get; set; }

    public virtual DbSet<MsMachineRegiman> MsMachineRegimen { get; set; }

    public virtual DbSet<MsMachinesGroup> MsMachinesGroups { get; set; }

    public virtual DbSet<MsMachinesOri> MsMachinesOris { get; set; }

    public virtual DbSet<MsStopCause> MsStopCauses { get; set; }

    public virtual DbSet<MsStopHistory> MsStopHistories { get; set; }

    public virtual DbSet<MsUserPreference> MsUserPreferences { get; set; }

    public virtual DbSet<NextNumberGen> NextNumberGens { get; set; }

    public virtual DbSet<WorkOrder> WorkOrders { get; set; }

    public virtual DbSet<WorkOrderComment> WorkOrderComments { get; set; }

    public virtual DbSet<WorkOrderJobType> WorkOrderJobTypes { get; set; }

    public virtual DbSet<WorkOrderLog> WorkOrderLogs { get; set; }

    public virtual DbSet<WorkOrderMaintenanceType> WorkOrderMaintenanceTypes { get; set; }

    public virtual DbSet<WorkOrderManager> WorkOrderManagers { get; set; }

    public virtual DbSet<WorkOrderMaterial> WorkOrderMaterials { get; set; }

    public virtual DbSet<WorkOrderPart> WorkOrderParts { get; set; }

    public virtual DbSet<WorkOrderStatusChange> WorkOrderStatusChanges { get; set; }

    public virtual DbSet<WorkOrderTask> WorkOrderTasks { get; set; }

    public virtual DbSet<WorkOrderWorker> WorkOrderWorkers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FscCalendario>(entity =>
        {
            entity.HasKey(e => e.Id).HasFillFactor(90);

            entity.ToTable("FSC_CALENDARIO");

            entity.Property(e => e.CdiaSemana)
                .IsRequired()
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnName("CDia_Semana");
            entity.Property(e => e.Cfecha)
                .HasColumnType("datetime")
                .HasColumnName("CFecha");
            entity.Property(e => e.Cferiado)
                .IsRequired()
                .HasMaxLength(1)
                .IsFixedLength()
                .HasDefaultValueSql("((0))", "DF_FSC_CALENDARIO_CFeriado")
                .HasColumnName("CFeriado");
            entity.Property(e => e.Cporcentaje)
                .HasColumnType("decimal(6, 2)")
                .HasColumnName("CPorcentaje");
        });

        modelBuilder.Entity<MachineDependence>(entity =>
        {
            entity.HasKey(e => e.MachineId);

            entity.ToTable("MACHINE_DEPENDENCES");

            entity.HasIndex(e => e.MachineId, "IX_MACHINE_DEPENDENCES");

            entity.HasIndex(e => e.FatherMachineId, "IX_MACHINE_DEPENDENCES_1");

            entity.Property(e => e.MachineId)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("MACHINE_ID");
            entity.Property(e => e.DualDependence).HasColumnName("DUAL_DEPENDENCE");
            entity.Property(e => e.FatherMachineId)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("FATHER_MACHINE_ID");

            entity.HasOne(d => d.FatherMachine).WithMany(p => p.InverseFatherMachine)
                .HasForeignKey(d => d.FatherMachineId)
                .HasConstraintName("FK_MACHINE_DEPENDENCES_MACHINE_DEPENDENCES");
        });

        modelBuilder.Entity<MsMachine>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.ToTable("MS_MACHINES");

            entity.Property(e => e.Id)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("ID");
            entity.Property(e => e.Criticidad)
                .HasDefaultValue(0, "DF_MS_MACHINES_CRITICIDAD")
                .HasColumnName("CRITICIDAD");
            entity.Property(e => e.Deleted)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("DELETED");
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .HasColumnName("DESCRIPTION");
            entity.Property(e => e.Disparador).HasColumnName("DISPARADOR");
            entity.Property(e => e.GroupId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("GROUP_ID");
            entity.Property(e => e.IdCategory).HasColumnName("ID_CATEGORY");
            entity.Property(e => e.Inviewer)
                .HasDefaultValue(false, "DF_MS_MACHINES_INVIEWER")
                .HasColumnName("INVIEWER");
            entity.Property(e => e.Order)
                .HasDefaultValue(0, "DF_MS_MACHINES_ORDER")
                .HasColumnName("ORDER");
            entity.Property(e => e.Regimen)
                .HasDefaultValue(0, "DF_MS_MACHINES_REGIMEN")
                .HasColumnName("REGIMEN");
            entity.Property(e => e.Status)
                .HasDefaultValue(1, "DF_MS_MACHINES_STATUS")
                .HasColumnName("STATUS");
            entity.Property(e => e.VmId)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("VM_ID");
        });

        modelBuilder.Entity<MsMachineCategory>(entity =>
        {
            entity.ToTable("MS_MACHINE_CATEGORIES");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("DESCRIPTION");
        });

        modelBuilder.Entity<MsMachineCriticidad>(entity =>
        {
            entity.ToTable("MS_MACHINE_CRITICIDAD");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .HasColumnName("DESCRIPTION");
        });

        modelBuilder.Entity<MsMachinePart>(entity =>
        {
            entity.HasKey(e => e.PartId).HasName("PK_MS_MACHINE_PARTS_1");

            entity.ToTable("MS_MACHINE_PARTS");

            entity.Property(e => e.PartId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PART_ID");
            entity.Property(e => e.Deleted)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("DELETED");
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("DESCRIPTION");
            entity.Property(e => e.FatherPartId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("FATHER_PART_ID");
            entity.Property(e => e.MachineId)
                .IsRequired()
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("MACHINE_ID");

            entity.HasOne(d => d.FatherPart).WithMany(p => p.InverseFatherPart)
                .HasForeignKey(d => d.FatherPartId)
                .HasConstraintName("FK_MS_MACHINE_PARTS_MS_MACHINE_PARTS");

            entity.HasOne(d => d.Machine).WithMany(p => p.MsMachineParts)
                .HasForeignKey(d => d.MachineId)
                .HasConstraintName("FK_MS_MACHINE_PARTS_MS_MACHINES");
        });

        modelBuilder.Entity<MsMachinePartsShDocument>(entity =>
        {
            entity.ToTable("MS_MACHINE_PARTS_SH_DOCUMENTS");

            entity.HasIndex(e => e.MachineId, "IX_MS_MACHINE_PARTS_SH_DOCUMENTS_1");

            entity.HasIndex(e => e.PartId, "IX_MS_MACHINE_PARTS_SH_DOCUMENTS_2");

            entity.HasIndex(e => new { e.MachineId, e.PartId }, "IX_MS_MACHINE_PARTS_SH_DOCUMENTS_3");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Deleted)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("DELETED");
            entity.Property(e => e.DocumentDescription)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("DOCUMENT_DESCRIPTION");
            entity.Property(e => e.DocumentId)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("DOCUMENT_ID");
            entity.Property(e => e.DocumentUrl)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("DOCUMENT_URL");
            entity.Property(e => e.MachineId)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("MACHINE_ID");
            entity.Property(e => e.PartId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PART_ID");
        });

        modelBuilder.Entity<MsMachineRegiman>(entity =>
        {
            entity.ToTable("MS_MACHINE_REGIMEN");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .HasColumnName("DESCRIPTION");
            entity.Property(e => e.Desde)
                .HasColumnType("datetime")
                .HasColumnName("DESDE");
            entity.Property(e => e.Hasta)
                .HasColumnType("datetime")
                .HasColumnName("HASTA");
        });

        modelBuilder.Entity<MsMachinesGroup>(entity =>
        {
            entity.ToTable("MS_MACHINES_GROUPS");

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ID");
            entity.Property(e => e.Order)
                .HasDefaultValue(0, "DF_MS_MACHINES_GROUPS_ORDER")
                .HasColumnName("ORDER");
        });

        modelBuilder.Entity<MsMachinesOri>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("MS_MACHINES_ORI");

            entity.Property(e => e.Criticidad).HasColumnName("CRITICIDAD");
            entity.Property(e => e.Deleted)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("DELETED");
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .HasColumnName("DESCRIPTION");
            entity.Property(e => e.GroupId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("GROUP_ID");
            entity.Property(e => e.Id)
                .IsRequired()
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("ID");
            entity.Property(e => e.IdCategory).HasColumnName("ID_CATEGORY");
            entity.Property(e => e.Inviewer).HasColumnName("INVIEWER");
            entity.Property(e => e.Order).HasColumnName("ORDER");
            entity.Property(e => e.Regimen).HasColumnName("REGIMEN");
            entity.Property(e => e.Status).HasColumnName("STATUS");
            entity.Property(e => e.VmId)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("VM_ID");
        });

        modelBuilder.Entity<MsStopCause>(entity =>
        {
            entity.ToTable("MS_STOP_CAUSES");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("DESCRIPTION");
        });

        modelBuilder.Entity<MsStopHistory>(entity =>
        {
            entity.ToTable("MS_STOP_HISTORY");

            entity.HasIndex(e => new { e.IdMachine, e.Stop }, "_dta_index_MS_STOP_HISTORY_10_23671132__K2_K3_1_4");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Comments).HasColumnName("COMMENTS");
            entity.Property(e => e.Deleted)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasDefaultValue("N", "DF_MS_STOP_HISTORY_DELETED")
                .HasColumnName("DELETED");
            entity.Property(e => e.IdMachine)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("ID_MACHINE");
            entity.Property(e => e.Start)
                .HasColumnType("datetime")
                .HasColumnName("START");
            entity.Property(e => e.Status)
                .HasDefaultValue(1, "DF_MS_STOP_HISTORY_STATUS")
                .HasColumnName("STATUS");
            entity.Property(e => e.Stop)
                .HasColumnType("datetime")
                .HasColumnName("STOP");
            entity.Property(e => e.Stopgen)
                .HasDefaultValue(false, "DF_MS_STOP_HISTORY_STOPPER")
                .HasColumnName("STOPGEN");
            entity.Property(e => e.User)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("USER");

            entity.HasOne(d => d.IdMachineNavigation).WithMany(p => p.MsStopHistories)
                .HasForeignKey(d => d.IdMachine)
                .HasConstraintName("FK_MS_STOP_HISTORY_MS_STOP_CAUSES");
        });

        modelBuilder.Entity<MsUserPreference>(entity =>
        {
            entity.HasKey(e => e.IdUser);

            entity.ToTable("MS_USER_PREFERENCES");

            entity.Property(e => e.IdUser)
                .HasMaxLength(30)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("ID_USER");
            entity.Property(e => e.ApplyStop).HasColumnName("APPLY_STOP");
            entity.Property(e => e.StopTime)
                .HasColumnType("decimal(18, 4)")
                .HasColumnName("STOP_TIME");
        });

        modelBuilder.Entity<NextNumberGen>(entity =>
        {
            entity.ToTable("NEXT_NUMBER_GEN");

            entity.HasIndex(e => new { e.TableName, e.ColumnName }, "IX_NEXT_NUMBER_GEN");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AlphaPrefix)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("ALPHA_PREFIX");
            entity.Property(e => e.AlphaSuffix)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("ALPHA_SUFFIX");
            entity.Property(e => e.ColumnName)
                .HasMaxLength(18)
                .IsUnicode(false)
                .HasColumnName("COLUMN_NAME");
            entity.Property(e => e.Description)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("DESCRIPTION");
            entity.Property(e => e.NextNumber).HasColumnName("NEXT_NUMBER");
            entity.Property(e => e.TableName)
                .HasMaxLength(18)
                .IsUnicode(false)
                .HasColumnName("TABLE_NAME");
        });

        modelBuilder.Entity<WorkOrder>(entity =>
        {
            entity.ToTable("WORK_ORDER", tb =>
                {
                    tb.HasTrigger("FSC_WO_CLOSE");
                    tb.HasTrigger("SendEmail");
                    tb.HasTrigger("SendEmailFundicion");
                    tb.HasTrigger("SendEmailFundicion-OLD");
                    tb.HasTrigger("SendEmailFundicionCierre");
                    tb.HasTrigger("SendEmailFundicionCierre-OLD");
                    tb.HasTrigger("SendEmailMecanizado");
                    tb.HasTrigger("SendEmailMecanizadoCierre");
                    tb.HasTrigger("SendEmailMecanizado_javy");
                    tb.HasTrigger("WORK_ORDER_LOG_I");
                    tb.HasTrigger("WORK_ORDER_LOG_U");
                });

            entity.HasIndex(e => new { e.IdStopCause, e.MachineId, e.MaintenanceType, e.CreateDate, e.Id }, "_dta_index_WORK_ORDER_10_1846297637__K12_K3_K6_K2_K1_8_9_10_11");

            entity.HasIndex(e => e.MachineId, "_dta_index_WORK_ORDER_10_1846297637__K3_1_2_4_5_6_7_9_10_12");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.AssignedTo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ASSIGNED_TO");
            entity.Property(e => e.CreateDate)
                .HasColumnType("datetime")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.FechaUpdate)
                .HasColumnType("datetime")
                .HasColumnName("FECHA_UPDATE");
            entity.Property(e => e.IdStopCause)
                .HasDefaultValue(8, "DF_WORK_ORDER_ID_STOP_CAUSE")
                .HasColumnName("ID_STOP_CAUSE");
            entity.Property(e => e.JobType)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasDefaultValue("Reparación", "DF_WORK_ORDER_JOB_TYPE")
                .HasColumnName("JOB_TYPE");
            entity.Property(e => e.MachineId)
                .IsRequired()
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("MACHINE_ID");
            entity.Property(e => e.MaintenanceType)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("MAINTENANCE_TYPE");
            entity.Property(e => e.Notificado)
                .HasDefaultValue(false)
                .HasColumnName("NOTIFICADO");
            entity.Property(e => e.Observations)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("OBSERVATIONS");
            entity.Property(e => e.PartId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PART_ID");
            entity.Property(e => e.PartList)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("PART_LIST");
            entity.Property(e => e.ProgrammingDate)
                .HasColumnType("datetime")
                .HasColumnName("PROGRAMMING_DATE");
            entity.Property(e => e.Status)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("STATUS");
            entity.Property(e => e.User)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("USER");
            entity.Property(e => e.UserCanceller)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("USER_CANCELLER");
            entity.Property(e => e.UserCloser)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("USER_CLOSER");
            entity.Property(e => e.UsuarioUpdate)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("USUARIO_UPDATE");
            entity.Property(e => e.WoFatherId).HasColumnName("WO_FATHER_ID");

            entity.HasOne(d => d.IdStopCauseNavigation).WithMany(p => p.WorkOrders)
                .HasForeignKey(d => d.IdStopCause)
                .HasConstraintName("FK_WORK_ORDER_MS_STOP_CAUSES");

            entity.HasOne(d => d.Machine).WithMany(p => p.WorkOrders)
                .HasForeignKey(d => d.MachineId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WORK_ORDER_MS_MACHINES");

            entity.HasOne(d => d.WoFather).WithMany(p => p.InverseWoFather)
                .HasForeignKey(d => d.WoFatherId)
                .HasConstraintName("FK_WORK_ORDER_WORK_ORDER");

            entity.HasMany(d => d.Stops).WithMany(p => p.Wos)
                .UsingEntity<Dictionary<string, object>>(
                    "WorkOrderStopsLink",
                    r => r.HasOne<MsStopHistory>().WithMany()
                        .HasForeignKey("StopId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_WORK_ORDER_STOPS_LINK_MS_STOP_HISTORY"),
                    l => l.HasOne<WorkOrder>().WithMany()
                        .HasForeignKey("WoId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_WORK_ORDER_STOPS_LINK_WORK_ORDER"),
                    j =>
                    {
                        j.HasKey("WoId", "StopId");
                        j.ToTable("WORK_ORDER_STOPS_LINK");
                        j.IndexerProperty<long>("WoId").HasColumnName("WO_ID");
                        j.IndexerProperty<int>("StopId").HasColumnName("STOP_ID");
                    });
        });

        modelBuilder.Entity<WorkOrderComment>(entity =>
        {
            entity.HasKey(e => new { e.WoId, e.Id });

            entity.ToTable("WORK_ORDER_COMMENTS");

            entity.Property(e => e.WoId).HasColumnName("WO_ID");
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Comment).HasColumnName("COMMENT");
            entity.Property(e => e.CreateDate)
                .HasColumnType("datetime")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.User)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("USER");

            entity.HasOne(d => d.Wo).WithMany(p => p.WorkOrderComments)
                .HasForeignKey(d => d.WoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WORK_ORDER_COMMENTS_WORK_ORDER");
        });

        modelBuilder.Entity<WorkOrderJobType>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("WORK_ORDER_JOB_TYPE");

            entity.Property(e => e.Id)
                .IsRequired()
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("ID");
        });

        modelBuilder.Entity<WorkOrderLog>(entity =>
        {
            entity.ToTable("WORK_ORDER_LOG");

            entity.HasIndex(e => e.WoId, "IX_WORK_ORDER_LOG");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AssignedTo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ASSIGNED_TO");
            entity.Property(e => e.CreateDate)
                .HasColumnType("datetime")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.FechaUpdate)
                .HasColumnType("datetime")
                .HasColumnName("FECHA_UPDATE");
            entity.Property(e => e.IdStopCause).HasColumnName("ID_STOP_CAUSE");
            entity.Property(e => e.JobType)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("JOB_TYPE");
            entity.Property(e => e.MachineId)
                .IsRequired()
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("MACHINE_ID");
            entity.Property(e => e.MaintenanceType)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("MAINTENANCE_TYPE");
            entity.Property(e => e.Observations)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("OBSERVATIONS");
            entity.Property(e => e.PartList)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("PART_LIST");
            entity.Property(e => e.ProgrammingDate)
                .HasColumnType("datetime")
                .HasColumnName("PROGRAMMING_DATE");
            entity.Property(e => e.Status)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("STATUS");
            entity.Property(e => e.User)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("USER");
            entity.Property(e => e.UserCanceller)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("USER_CANCELLER");
            entity.Property(e => e.UserCloser)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("USER_CLOSER");
            entity.Property(e => e.UsuarioUpdate)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("USUARIO_UPDATE");
            entity.Property(e => e.WoId).HasColumnName("WO_ID");

            entity.HasOne(d => d.IdStopCauseNavigation).WithMany(p => p.WorkOrderLogs)
                .HasForeignKey(d => d.IdStopCause)
                .HasConstraintName("FK_WORK_ORDER_LOG_MS_STOP_CAUSES");

            entity.HasOne(d => d.Machine).WithMany(p => p.WorkOrderLogs)
                .HasForeignKey(d => d.MachineId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WORK_ORDER_LOG_MS_MACHINES");

            entity.HasOne(d => d.Wo).WithMany(p => p.WorkOrderLogs)
                .HasForeignKey(d => d.WoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WORK_ORDER_LOG_WORK_ORDER");
        });

        modelBuilder.Entity<WorkOrderMaintenanceType>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("WORK_ORDER_MAINTENANCE_TYPE");

            entity.Property(e => e.Id)
                .IsRequired()
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("ID");
            entity.Property(e => e.WId)
                .ValueGeneratedOnAdd()
                .HasColumnName("W_ID");
        });

        modelBuilder.Entity<WorkOrderManager>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("WORK_ORDER_MANAGERS");

            entity.Property(e => e.JoinDate)
                .HasColumnType("datetime")
                .HasColumnName("JOIN_DATE");
            entity.Property(e => e.User)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("USER");
            entity.Property(e => e.WoId).HasColumnName("WO_ID");

            entity.HasOne(d => d.Wo).WithMany()
                .HasForeignKey(d => d.WoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WORK_ORDER_MANAGERS_WORK_ORDER");
        });

        modelBuilder.Entity<WorkOrderMaterial>(entity =>
        {
            entity.ToTable("WORK_ORDER_MATERIALS");

            entity.HasIndex(e => e.WoId, "IX_WORK_ORDER_MATERIALS");

            entity.HasIndex(e => e.PartId, "IX_WORK_ORDER_MATERIALS_1");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Observations)
                .IsUnicode(false)
                .HasColumnName("OBSERVATIONS");
            entity.Property(e => e.PartId)
                .IsRequired()
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("PART_ID");
            entity.Property(e => e.Quantity)
                .HasColumnType("decimal(18, 5)")
                .HasColumnName("QUANTITY");
            entity.Property(e => e.StockUm)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("STOCK_UM");
            entity.Property(e => e.TransactionDate)
                .HasColumnType("datetime")
                .HasColumnName("TRANSACTION_DATE");
            entity.Property(e => e.UnitPrice)
                .HasColumnType("decimal(18, 5)")
                .HasColumnName("UNIT_PRICE");
            entity.Property(e => e.User)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("USER");
            entity.Property(e => e.WoId).HasColumnName("WO_ID");

            entity.HasOne(d => d.Wo).WithMany(p => p.WorkOrderMaterials)
                .HasForeignKey(d => d.WoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WORK_ORDER_MATERIALS_WORK_ORDER");
        });

        modelBuilder.Entity<WorkOrderPart>(entity =>
        {
            entity.HasKey(e => new { e.WoId, e.MachineId, e.PartId });

            entity.ToTable("WORK_ORDER_PARTS");

            entity.HasIndex(e => e.MachineId, "_dta_index_WORK_ORDER_PARTS_10_28579190__K2");

            entity.Property(e => e.WoId).HasColumnName("WO_ID");
            entity.Property(e => e.MachineId)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("MACHINE_ID");
            entity.Property(e => e.PartId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PART_ID");

            entity.HasOne(d => d.Machine).WithMany(p => p.WorkOrderParts)
                .HasForeignKey(d => d.MachineId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WORK_ORDER_PARTS_MS_MACHINES");

            entity.HasOne(d => d.Part).WithMany(p => p.WorkOrderParts)
                .HasForeignKey(d => d.PartId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WORK_ORDER_PARTS_MS_MACHINE_PARTS");

            entity.HasOne(d => d.Wo).WithMany(p => p.WorkOrderParts)
                .HasForeignKey(d => d.WoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WORK_ORDER_PARTS_WORK_ORDER");
        });

        modelBuilder.Entity<WorkOrderStatusChange>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("WORK_ORDER_STATUS_CHANGE");

            entity.Property(e => e.MsGroupId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MS_GROUP_ID");
            entity.Property(e => e.MsMachineId)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("MS_MACHINE_ID");
            entity.Property(e => e.WoCloseDate)
                .HasColumnType("datetime")
                .HasColumnName("WO_CLOSE_DATE");
            entity.Property(e => e.WoId).HasColumnName("WO_ID");
            entity.Property(e => e.WoStatus)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("WO_STATUS");
        });

        modelBuilder.Entity<WorkOrderTask>(entity =>
        {
            entity.HasKey(e => new { e.WoId, e.TaskNo });

            entity.ToTable("WORK_ORDER_TASKS");

            entity.HasIndex(e => new { e.WoId, e.TaskNo, e.StartDate }, "IX_WORK_ORDER_TASKS");

            entity.Property(e => e.WoId).HasColumnName("WO_ID");
            entity.Property(e => e.TaskNo).HasColumnName("TASK_NO");
            entity.Property(e => e.CloseDate)
                .HasColumnType("datetime")
                .HasColumnName("CLOSE_DATE");
            entity.Property(e => e.CloseUser)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CLOSE_USER");
            entity.Property(e => e.CreateDate)
                .HasColumnType("datetime")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.EndDate)
                .HasColumnType("datetime")
                .HasColumnName("END_DATE");
            entity.Property(e => e.Observations)
                .IsUnicode(false)
                .HasColumnName("OBSERVATIONS");
            entity.Property(e => e.StartDate)
                .HasColumnType("datetime")
                .HasColumnName("START_DATE");
            entity.Property(e => e.User)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("USER");

            entity.HasOne(d => d.Wo).WithMany(p => p.WorkOrderTasks)
                .HasForeignKey(d => d.WoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WORK_ORDER_TASKS_WORK_ORDER");
        });

        modelBuilder.Entity<WorkOrderWorker>(entity =>
        {
            entity.HasKey(e => new { e.WoId, e.TaskNo, e.EmployeeId });

            entity.ToTable("WORK_ORDER_WORKERS");

            entity.HasIndex(e => e.EmployeeId, "IX_WORK_ORDER_WORKERS");

            entity.HasIndex(e => e.WoId, "IX_WORK_ORDER_WORKERS_1");

            entity.HasIndex(e => new { e.WoId, e.TaskNo }, "IX_WORK_ORDER_WORKERS_2");

            entity.Property(e => e.WoId).HasColumnName("WO_ID");
            entity.Property(e => e.TaskNo).HasColumnName("TASK_NO");
            entity.Property(e => e.EmployeeId)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("EMPLOYEE_ID");
            entity.Property(e => e.DepthHours)
                .HasDefaultValue(0m, "DF_WORK_ORDER_WORKERS_DEPTH_HOURS")
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("DEPTH_HOURS");
            entity.Property(e => e.ExtraHours)
                .HasDefaultValue(0m, "DF_WORK_ORDER_WORKERS_EXTRA_HOURS")
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("EXTRA_HOURS");
            entity.Property(e => e.HighHours)
                .HasDefaultValue(0m, "DF_WORK_ORDER_WORKERS_HIGH_HOURS")
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("HIGH_HOURS");
            entity.Property(e => e.User)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("USER");
            entity.Property(e => e.WorkedHours)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("WORKED_HOURS");

            entity.HasOne(d => d.WorkOrderTask).WithMany(p => p.WorkOrderWorkers)
                .HasForeignKey(d => new { d.WoId, d.TaskNo })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WORK_ORDER_WORKERS_WORK_ORDER_TASKS");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
