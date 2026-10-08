"""Deferred child slot for running native Python bodies inside original ScriptHost controllers."""
import collections
import copy

from .runtime import _spf_manager_type

_spf_manager_type()  # puts the vendored SPFLib on sys.path
import SPFLib.SPFSQL3 as legacy  # noqa: E402


class DeferredChildTask(legacy.SPFTaskBase):
    """
    Placeholder child of a controller in a portable Python session. The controller yields it where it
    would execute its child tasks; the session then runs Python statements there. Substitutions the
    controller applies to its children are kept in 'layers' and replayed on each of those statements.
    """
    isDeferredSlot = True

    def __init__(self):
        super().__init__("", None)
        self.layers = []
        self.errorHandler = None  # set by executeChildTasksSteps; None means errors propagate to the controller

    def substituteMacro(self, Rowidx, MyMode, currentRow=None):
        self.layers.append(MacroLayer(self.parentMacTables, Rowidx, MyMode, currentRow))

    def __deepcopy__(self, memo):
        clone = copy.copy(self)
        clone.layers = list(self.layers)
        return clone


class MacroLayer:
    """
    One START-MACRO substitution as StartMacroTask applies it to its children: text uses the macro
    tables present at that moment, while nested macros receive the shared (live) table dictionary.
    """

    def __init__(self, parentMacTables, Rowidx, MyMode, currentRow=None):
        self.live = parentMacTables
        self.tables = collections.OrderedDict(parentMacTables)
        self.Rowidx, self.MyMode, self.currentRow = Rowidx, MyMode, currentRow

    def __call__(self, tasks):
        for task in tasks:
            if task.isDeferredSlot or task.isControlerEndTask is True:
                continue  # end tasks never substitute macros; slots inherit this layer from their session
            task.SPFTaskItem = task.Substitute_Macro(
                task.SPFTaskItem, parentMacTables=self.tables, Rowidx=self.Rowidx, MyMode=self.MyMode,
                currentRow=self.currentRow)
            task.parentMacTables = self.live
            if type(task) is not legacy.StartMacroTask:
                self(task.childTasksList)  # a nested START-MACRO substitutes its own children when it executes
        return tasks
